using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;
using Veda.Application.Ports.Storage.Encryption;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Veda.Infrastructure.ServiceImplementations;

public class PgpFileEncryptor : IFileEncryptor
{
    public MemoryStream Encrypt(Stream inputData, string passphrase)
    {
        MemoryStream outputData = new MemoryStream();
        PgpEncryptedDataGenerator encryptedDataGenerator = new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.Cast5, new SecureRandom());
        encryptedDataGenerator.AddMethodUtf8(passphrase.ToCharArray(), HashAlgorithmTag.Sha256);

        using (Stream encryptedOut = encryptedDataGenerator.Open(outputData, new byte[1 << 16]))
        {
            PgpCompressedDataGenerator compressedDataGenerator = new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip);
            Stream compressedOutput = compressedDataGenerator.Open(encryptedOut);

            PgpLiteralDataGenerator literalDataGenerator = new PgpLiteralDataGenerator();
            Stream literalOut = literalDataGenerator.Open(compressedOutput, PgpLiteralData.Binary, "filename", inputData.Length, DateTime.UtcNow);
            Streams.PipeAll(inputData, literalOut);

            literalDataGenerator.Close();
            compressedDataGenerator.Close();
        }

        return outputData;
    }

    public MemoryStream DecryptData(Stream encryptedData, string passphrase)
    {
        if (encryptedData.CanSeek)
        {
            encryptedData.Position = 0;
        }
        
        PgpObjectFactory pgpF = new PgpObjectFactory(PgpUtilities.GetDecoderStream(encryptedData));
        PgpEncryptedDataList enc = null;
        PgpObject o = pgpF.NextPgpObject();
        if (o is PgpEncryptedDataList)
            enc = (PgpEncryptedDataList)o;
        else
            enc = (PgpEncryptedDataList)pgpF.NextPgpObject();

        PgpPbeEncryptedData pbe = (PgpPbeEncryptedData)enc[0];
        Stream clear = pbe.GetDataStream(passphrase.ToCharArray());

        PgpObjectFactory plainFact = new PgpObjectFactory(clear);
        PgpCompressedData cData = (PgpCompressedData)plainFact.NextPgpObject();

        Stream dataIn = cData.GetDataStream();
        PgpObjectFactory pgpFact = new PgpObjectFactory(dataIn);

        PgpLiteralData ld = (PgpLiteralData)pgpFact.NextPgpObject();
        Stream unc = ld.GetInputStream();

        var outputStream = new MemoryStream();
        Streams.PipeAll(unc, outputStream);

        return outputStream;
    }
}