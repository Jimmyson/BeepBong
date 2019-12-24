using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AcoustID;
using BeepBong.Application.ViewModels;
using BeepBong.Domain.Models;
using Javi.MediaInfo;
using NAudio.Wave;

namespace BeepBong.SampleUpload
{
    public static class AudioProcessing
    {
        public static string ProcessFile(string filePath)
        {
            try
            {
                FileStream fs = File.OpenRead(filePath);
                byte[] dataBytes = new byte[1024];
                short[] shorts = new short[dataBytes.Length];

                ChromaContext ctx = new ChromaContext();
                ctx.Start(44100, 2);

                while (fs.Read(dataBytes, 0, dataBytes.Length) > 0)
                {
                    Buffer.BlockCopy(dataBytes, 0, shorts, 0, shorts.Length);
                    ctx.Feed(shorts, shorts.Length);
                }

                ctx.Finish();
                shorts = null;
                dataBytes = null;

                int[] hash = ctx.GetRawFingerprint();
                ctx = null;

                var md5 = MD5.Create();

                byte[] result = new byte[hash.Length * sizeof(int)];
                Buffer.BlockCopy(hash, 0, result, 0, result.Length);
                hash = null;

                var encByte = md5.ComputeHash(result);
                result = null;
                md5.Dispose();

                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < encByte.Length; i++)
                {
                    sBuilder.Append(encByte[i].ToString("x2"));
                }

                encByte = null;

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static SampleCreateViewModel CreateSample(string filePath)
        {
            MediaInfo mediaInfo = new MediaInfo(@".\lib\MediaInfo.dll");
            mediaInfo.ReadMediaInformation(filePath);

            AudioFileReader nAudio = new AudioFileReader(filePath);

            string bitDepth = null;

            foreach (string detail in mediaInfo.Information)
            {
                if (detail.Contains("Bit depth"))
                {
                    bitDepth = detail.Substring(detail.IndexOf(':') + 1).Trim();
                }
            }

            SampleCreateViewModel sample = new SampleCreateViewModel
            {
                SampleRate = mediaInfo.Audio[0].SamplingRate.ToString(),
                SampleCount = "Unknown",
                AudioChannelCount = mediaInfo.Audio[0].Channels.ToString(),
                BitRate = mediaInfo.Audio[0].BitRate.ToString(),
                Codec = mediaInfo.Audio[0].Format,
                BitDepth = bitDepth ?? "Unknown"
            };

            BitRateModeEnum bitRateMode;
            CompressionEnum compression;

            Enum.TryParse(mediaInfo.Audio[0].BitRateModeAbbreviation, out bitRateMode);
            Enum.TryParse(mediaInfo.Audio[0].CompressionMode, out compression);

            sample.BitRateMode = bitRateMode;
            sample.Compression = compression;

            mediaInfo = null;

            return sample;
        }
    }
}
