using System.Drawing;
using System.Drawing.Imaging;
using CppSharp.Generators;
using CppSharp;
using CppSharp.AST;
using Vpx;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    internal class Program
    {
        private static UInt32 VP8_FOURCC = 0x30385056;
        private static UInt32 VP9_FOURCC = 0x30395056;
        static unsafe void Main(string[] args)
        {
            string imagePath = "C:\\Users\\29239\\Pictures\\微信图片_20240327140130.jpg";
           var fils= Directory.CreateDirectory(AppContext.BaseDirectory).GetFiles("frame*.jpg");
            
             using (var fs=File.Open("tem.ivf",FileMode.Create))
                {
                var codec_iface = vp8cx.VpxCodecVp9Cx();
                var config = new VpxCodecEncCfg();

                // config.GErrorResilient=
                var err = vpx_encoder.VpxCodecEncConfigDefault(codec_iface, config, 0);
                config.GW = 1920;
                config.GH =1088;
                config.GTimebase.Num = 1;
                config.GTimebase.Den = 30;
                config.RcTargetBitrate = 200;
                config.RcEndUsage = VpxRcMode.VPX_VBR;
                //config.RcTargetBitrate = 4 * 1024;
                config.RcMaxQuantizer = 20;
                config.RcMinQuantizer = 0;
               VpxCodecCtx codec = new VpxCodecCtx();
               
                var version = vpx_codec.VpxCodecVersion();
                var ress = vpx_codec.VpxCodecVersionStr();
                var VPX_IMG_FMT_HIGHBITDEPTH = 0x800;
                err = vpx_encoder.VpxCodecEncInitVer(codec, codec_iface, config, 0, 16 + 9 + 7 + 2);
                var errStr = vpx_codec.VpxCodecErrorDetail(codec);
                WriteHeader(fs, VP9_FOURCC, 0, (short)config.GW, (short)config.GH, 30, 1);
                int frame_count = 0;
                var times = Stopwatch.GetTimestamp();
                int i = 0;
                foreach (var fileInfo in fils)
                    {

                    using (Bitmap bitmap = new Bitmap(fileInfo.Name))
                    {
                        var loc = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                      bitmap.PixelFormat);
                        var _ySize = loc.Width * loc.Height;
                        var _uvSize = _ySize >> 2;
                        var _yStride = loc.Width;
                        var _uvStride = _yStride >> 1;
                        var yuv = (nint)NativeMemory.Alloc((uint)(_ySize + _uvSize + _uvSize));
                        Lennox.LibYuvSharp.LibYuv.RGB24ToI420((byte*)loc.Scan0, loc.Stride, (byte*)yuv, _yStride,
                           (byte*)(yuv + _ySize), _uvStride, (byte*)(yuv + _ySize + _uvSize), _uvStride, loc.Width,
                           loc.Height);
                     
                        VpxImage vpx = new VpxImage();

                        var img = VpxImage.VpxImgWrap(vpx, VpxImgFmt.VPX_IMG_FMT_I420, (uint)loc.Width, (uint)loc.Height, 1,
                            (byte*)yuv.ToPointer());
                        // NativeMemory.Copy(yuv.ToPointer(),vpx.ImgData, (uint)(_ySize + _uvSize + _uvSize));
                        //img.ImgData = (byte*)yuv.ToPointer();

                     
                        // img.Planes=new byte*[]{ (byte*)IntPtr.Zero.ToPointer(), (byte*)yuv, (byte*)(yuv + _ySize), (byte*)(yuv + _ySize + _uvSize) };
                        //img.Stride = new int[] { 0,_yStride ,_uvStride,_uvStride};
                     
                        
                        
                            int flag = 0;
                            if (i % 20 == 0)
                            {
                                flag = 1;
                            }
                            else
                            {
                                flag = 0;
                            }

                            var count = encode_frame(codec, img, i, flag, fs, ref frame_count);
                            Console.WriteLine($"{Stopwatch.GetElapsedTime(times)}:{frame_count}");


                        
                        bitmap.UnlockBits(loc);
                        VpxImage.VpxImgFree(img);
                        NativeMemory.Free(yuv.ToPointer());
                       

                       

                       
                    }

                    i++;
                    }
                while (encode_frame(codec, null, -1, 0, fs, ref frame_count) > 0)
                {

                }
                fs.Seek(0, SeekOrigin.Begin);
                WriteHeader(fs, VP9_FOURCC, (uint)frame_count, (short)config.GW, (short)config.GH, 30, 1);
            }
            
              
          
       
          
         
            
            //ConsoleDriver.Run(new rav1e());
            Console.WriteLine("Hello, World!");
        }

        private unsafe static int encode_frame(VpxCodecCtx codec, VpxImage img,int i,int flags,Stream stream,ref int frame_count)
        {
            var VPX_FRAME_IS_KEY = 0x1u;
            var err = vpx_encoder.VpxCodecEncode(codec, img, i, 1, flags, 1000000);
           var errStr = vpx_codec.VpxCodecErrorDetail(codec);
            IntPtr iter = IntPtr.Zero;
            VpxCodecCxPkt pkt;
            int got_pkts = 0;
            while ((pkt = vpx_encoder.VpxCodecGetCxData(codec, &iter)) != null)
            {
                got_pkts = 1;
                if (pkt.Kind == VpxCodecCxPktKind.VPX_CODEC_CX_FRAME_PKT)
                {
                    var keyframe = (pkt.data.frame.Flags & VPX_FRAME_IS_KEY) != 0;
                    ivf_write_frame_header(stream, pkt.data.frame.Pts, pkt.data.frame.Sz);
                    stream.Write(new ReadOnlySpan<byte>(pkt.data.frame.Buf.ToPointer(), (int)pkt.data.frame.Sz));
                    frame_count++;
                    //if (!vpx_video_writer_write_frame(writer, pkt.data.frame.Buf,`kjm nbv
                    //        pkt.data.frame.Sz,
                    //        pkt.data.frame.Pts))
                    //{

                    //}
                }
            }

            return got_pkts;
        }
        private static void WriteHeader(Stream stream,  uint fourcc,
        uint frame_cnt, int frame_width,
        int frame_height,
            uint den,uint num)
        {
            byte[] header = new byte[32];
            header[0] = 68;
            header[1] = 75;
            header[2] = 73;
            header[3] = 70;
            Put16(header,4,0);// version
            Put16(header,6,32);// header size
            Put32(header,8, fourcc);  // fourcc
            Put16(header, 12, (short)frame_width);// width
            Put16(header, 14, (short)frame_height);// height
            Put32(header , 16, den);  // rate
            Put32(header, 20, num);  // scale
            Put32(header, 24, frame_cnt);  // length
            Put32(header, 28, 0);  // unused
            stream.Write(header);
        }

        static void ivf_write_frame_header(Stream stream, long pts, ulong frame_size)
        {
            byte[] header = new byte[12];
            Put32(header,0, (uint)frame_size);
            Put32(header, 4, (uint)(pts & 0xFFFFFFFF));
            Put32(header, 8, (uint)(pts >> 32));
            stream.Write(header);
        }
        private static void Put16(byte[] buff, int index, short value)
        {
            buff[index] = (byte)((value >> 0) & 0xff);
            buff[index+1] = (byte)((value >> 8) & 0xff);
        }
        private static void Put32(byte[] buff, int index, uint value)
        {
            buff[index] = (byte)((value >> 0) & 0xff);
            buff[index + 1] = (byte)((value >> 8) & 0xff);
            buff[index + 2] = (byte)((value >> 16) & 0xff);
            buff[index + 3] = (byte)((value >> 24) & 0xff);
        }
    }
    public unsafe partial class ExportBindings
    {
        [LibraryImport("vpx.dll", EntryPoint = "vpx_codec_build_config", StringMarshalling = StringMarshalling.Utf8)]
        internal static partial IntPtr vpx_codec_build_config();
        [LibraryImport("vpx.dll", EntryPoint = "get_vpx_encoder_by_name", StringMarshalling = StringMarshalling.Utf8)]
        internal static partial IntPtr get_vpx_encoder_by_name(string name);
    }
    public class rav1e : ILibrary
    {
        public void Preprocess(Driver driver, ASTContext ctx)
        {

        }

        public void Postprocess(Driver driver, ASTContext ctx)
        {

        }

        public void Setup(Driver driver)
        {
            var options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;
            var module = options.AddModule("Vpx");
            module.IncludeDirs.Add(@"C:\Users\29239\Downloads\msvc\include\vpx");
            module.Headers.Add("vpx_codec.h");
            module.Headers.Add("vpx_decoder.h");
            module.Headers.Add("vpx_encoder.h");
            module.Headers.Add("vpx_ext_ratectrl.h");
            module.Headers.Add("vpx_frame_buffer.h");
            module.Headers.Add("vpx_image.h");

            module.Headers.Add("vpx_integer.h");
            module.Headers.Add("vpx_tpl.h");
            module.Headers.Add("vp8.h");
            module.Headers.Add("vp8cx.h");
            module.Headers.Add("vp8dx.h");
            //module.LibraryDirs.Add(@"D:\迅雷下载\rav1e-windows-msvc-generic\rav1e-windows-msvc-sdk\lib");
            //module.Libraries.Add("rav1e.lib");
        }

        public void SetupPasses(Driver driver)
        {

        }
    }
}
