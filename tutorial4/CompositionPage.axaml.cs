using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Rendering.Composition.Animations;
using Avalonia.Rendering.Composition;
using Avalonia.Animation;
using Avalonia.Interactivity;
using Avalonia.Media.Immutable;
using Avalonia.VisualTree;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia.Media.Imaging;
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
using Microsoft.CodeAnalysis.Text;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Threading.Tasks;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;
using System.Linq;
using System.Resources;
using Vortice.MediaFoundation;
using SharpGen.Runtime;
using System.Security.Cryptography;

namespace Tutorial4;

public partial class CompositionPage : UserControl
{
    private ImplicitAnimationCollection? _implicitAnimations;
    private CompositionCustomVisual? _customVisual;
    private CompositionSolidColorVisual? _solidVisual;
    private CompositionCustomVisual? _customImageVisual;
    public CompositionPage()
    {
        InitializeComponent();
        AttachAnimatedSolidVisual(this.FindControl<Control>("SolidVisualHost")!);
        AttachCustomVisual(this.FindControl<Control>("CustomVisualHost")!);
        AttachCustomImageVisual(CustomImageVisualHost);
    }

   

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        this.Get<ItemsControl>("Items").ItemsSource = CreateColorItems();

    }

    private static List<CompositionPageColorItem> CreateColorItems()
    {
        var list = new List<CompositionPageColorItem>();

        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 255, 185, 0)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 231, 72, 86)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 0, 120, 215)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 0, 153, 188)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 122, 117, 116)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 118, 118, 118)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 255, 141, 0)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 232, 17, 35)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 0, 99, 177)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 45, 125, 154)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 93, 90, 88)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 76, 74, 72)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 247, 99, 12)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 234, 0, 94)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 142, 140, 216)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 0, 183, 195)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 104, 118, 138)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 105, 121, 126)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 202, 80, 16)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 195, 0, 82)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 107, 105, 214)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 3, 131, 135)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 81, 92, 107)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 74, 84, 89)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 218, 59, 1)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 227, 0, 140)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 135, 100, 184)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 0, 178, 148)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 86, 124, 115)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 100, 124, 100)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 239, 105, 80)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 191, 0, 119)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 116, 77, 169)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 1, 133, 116)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 72, 104, 96)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 82, 94, 84)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 209, 52, 56)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 194, 57, 179)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 177, 70, 194)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 0, 204, 106)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 73, 130, 5)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 132, 117, 69)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 255, 67, 67)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 154, 0, 137)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 136, 23, 152)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 16, 137, 62)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 16, 124, 16)));
        list.Add(new CompositionPageColorItem(Color.FromArgb(255, 126, 115, 95)));

        return list;
    }

    private void EnsureImplicitAnimations()
    {
        if (_implicitAnimations == null)
        {
            var compositor = ElementComposition.GetElementVisual(this)!.Compositor;

            var offsetAnimation = compositor.CreateVector3KeyFrameAnimation();
            offsetAnimation.Target = "Offset";
            offsetAnimation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(400);

            var rotationAnimation = compositor.CreateScalarKeyFrameAnimation();
            rotationAnimation.Target = "RotationAngle";
            rotationAnimation.InsertKeyFrame(.5f, 0.160f);
            rotationAnimation.InsertKeyFrame(1f, 0f);
            rotationAnimation.Duration = TimeSpan.FromMilliseconds(400);

            var animationGroup = compositor.CreateAnimationGroup();
            animationGroup.Add(offsetAnimation);
            animationGroup.Add(rotationAnimation);

            _implicitAnimations = compositor.CreateImplicitAnimationCollection();
            _implicitAnimations["Offset"] = animationGroup;
        }
    }

    public static void SetEnableAnimations(Border border, bool value)
    {
        var page = border.FindAncestorOfType<CompositionPage>();
        if (page == null)
        {
            border.AttachedToVisualTree += delegate { SetEnableAnimations(border, true); };
            return;
        }

        if (ElementComposition.GetElementVisual(page) == null)
            return;

        page.EnsureImplicitAnimations();
        if (border.GetVisualParent() is Visual visualParent
            && ElementComposition.GetElementVisual(visualParent) is CompositionVisual compositionVisual)
        {
            compositionVisual.ImplicitAnimations = page._implicitAnimations;
        }
    }

    void AttachAnimatedSolidVisual(Visual v)
    {
        void Update()
        {
            if (_solidVisual == null)
                return;
            _solidVisual.Size = new(v.Bounds.Width / 3, v.Bounds.Height / 3);
            _solidVisual.Offset = new(v.Bounds.Width / 3, v.Bounds.Height / 3, 0);
        }
        v.AttachedToVisualTree += delegate
        {
            var compositor = ElementComposition.GetElementVisual(v)?.Compositor;
            if (compositor == null || _solidVisual?.Compositor == compositor)
                return;
            _solidVisual = compositor.CreateSolidColorVisual();
            ElementComposition.SetElementChildVisual(v, _solidVisual);
            _solidVisual.Color = Colors.Red;
            var animation = _solidVisual.Compositor.CreateColorKeyFrameAnimation();
            animation.InsertKeyFrame(0, Colors.Red);
            animation.InsertKeyFrame(0.5f, Colors.Blue);
            animation.InsertKeyFrame(1, Colors.Green);
            animation.Duration = TimeSpan.FromSeconds(5);
            animation.IterationBehavior = AnimationIterationBehavior.Forever;
            animation.Direction = PlaybackDirection.Alternate;
            _solidVisual.StartAnimation("Color", animation);

            _solidVisual.AnchorPoint = new(0, 0);

            var scale = _solidVisual.Compositor.CreateVector3KeyFrameAnimation();
            scale.Duration = TimeSpan.FromSeconds(5);
            scale.IterationBehavior = AnimationIterationBehavior.Forever;
            scale.InsertKeyFrame(0, new Vector3(1, 1, 0));
            scale.InsertKeyFrame(0.5f, new Vector3(1.5f, 1.5f, 0));
            scale.InsertKeyFrame(1, new Vector3(1, 1, 0));

            _solidVisual.StartAnimation("Scale", scale);

            var center =
                _solidVisual.Compositor.CreateExpressionAnimation(
                    "Vector3(this.Target.Size.X * 0.5, this.Target.Size.Y * 0.5, 1)");
            _solidVisual.StartAnimation("CenterPoint", center);
            Update();
        };
        v.PropertyChanged += (_, a) =>
        {
            if (a.Property == BoundsProperty)
                Update();
        };
    }

    void AttachCustomVisual(Visual v)
    {
        void Update()
        {
            if (_customVisual == null)
                return;
            var h = (float)Math.Min(v.Bounds.Height, v.Bounds.Width / 3);
            _customVisual.Size = new(v.Bounds.Width, h);
            _customVisual.Offset = new(0, (v.Bounds.Height - h) / 2, 0);
        }
        v.AttachedToVisualTree += delegate
        {
            var compositor = ElementComposition.GetElementVisual(v)?.Compositor;
            if (compositor == null || _customVisual?.Compositor == compositor)
                return;
            _customVisual = compositor.CreateCustomVisual(new CustomVisualHandler());
            ElementComposition.SetElementChildVisual(v, _customVisual);
            _customVisual.SendHandlerMessage(CustomVisualHandler.StartMessage);
            Update();
        };

        v.PropertyChanged += (_, a) =>
        {
            if (a.Property == BoundsProperty)
                Update();
        };
    }
    private void AttachCustomImageVisual(Control v)
    {
        void Update()
        {
            if (_customImageVisual == null)
                return;
            var h = (float)Math.Min(v.Bounds.Height, v.Bounds.Width / 3);
            _customImageVisual.Size = new(v.Bounds.Width, v.Bounds.Width);
            _customImageVisual.Offset = new(0, 0, 0);
        }
        v.AttachedToVisualTree += delegate
        {
            var compositor = ElementComposition.GetElementVisual(v)?.Compositor;
            if (compositor == null || _customVisual?.Compositor == compositor)
                return;
            _customImageVisual = compositor.CreateCustomVisual(new CustomImageVisualHandler());
            ElementComposition.SetElementChildVisual(v, _customImageVisual);
            _customImageVisual.SendHandlerMessage(CustomImageVisualHandler.StartMessage);
            Update();
        };

        v.PropertyChanged += (_, a) =>
        {
            if (a.Property == BoundsProperty)
                Update();
        };
    }
    class CustomVisualHandler : CompositionCustomVisualHandler
    {
        private TimeSpan _animationElapsed;
        private TimeSpan? _lastServerTime;
        private bool _running;

        public static readonly object StopMessage = new(), StartMessage = new();

        public override void OnRender(ImmediateDrawingContext drawingContext)
        {
            if (_running)
            {
                if (_lastServerTime.HasValue) _animationElapsed += (CompositionNow - _lastServerTime.Value);
                _lastServerTime = CompositionNow;
            }

            const int cnt = 20;
            var maxPointSizeX = EffectiveSize.X / (cnt * 1.6);
            var maxPointSizeY = EffectiveSize.Y / 4;
            var pointSize = Math.Min(maxPointSizeX, maxPointSizeY);
            var animationLength = TimeSpan.FromSeconds(4);
            var animationStage = _animationElapsed.TotalSeconds / animationLength.TotalSeconds;

            var sinOffset = Math.Cos(_animationElapsed.TotalSeconds) * 1.5;

            for (var c = 0; c < cnt; c++)
            {
                var stage = (animationStage + (double)c / cnt) % 1;
                var colorStage =
                    (animationStage + (Math.Sin(_animationElapsed.TotalSeconds * 2) + 1) / 2 + (double)c / cnt) % 1;
                var posX = (EffectiveSize.X + pointSize * 3) * stage - pointSize;
                var posY = (EffectiveSize.Y - pointSize) * (1 + Math.Sin(stage * 3.14 * 3 + sinOffset)) / 2 + pointSize / 2;
                var opacity = Math.Sin(stage * 3.14);


                drawingContext.DrawEllipse(new ImmutableSolidColorBrush(Color.FromArgb(
                        255,
                        (byte)(255 - 255 * colorStage),
                        (byte)(255 * Math.Abs(0.5 - colorStage) * 2),
                        (byte)(255 * colorStage)
                    ), opacity), null,
                    new Point(posX, posY), pointSize / 2, pointSize / 2);
            }

        }

        public override void OnMessage(object message)
        {
            if (message == StartMessage)
            {
                _running = true;
                _lastServerTime = null;
                RegisterForNextAnimationFrameUpdate();
            }
            else if (message == StopMessage)
                _running = false;
        }

        public override void OnAnimationFrameUpdate()
        {
            if (_running)
            {
                Invalidate();
                RegisterForNextAnimationFrameUpdate();
            }
        }
    }
    class CustomImageVisualHandler : CompositionCustomVisualHandler
    {
        private TimeSpan _animationElapsed;
        private TimeSpan? _lastServerTime;
        private bool _running;

        public static readonly object StopMessage = new(), StartMessage = new();
        private SKBitmap orbitmap;
     
        private int bitsize;
        private long grapIndex;
        private long dispIndex;
        private IntPtr ImagePtr;
        public CustomImageVisualHandler()
        {
           
            
            ScreenGrap();
           // VideoGrap();
        }
        private unsafe void ScreenGrap()
        {

            ID3D11Device device = null;
            IDXGIOutput1 seleOutput1 = null;
            IDXGIFactory1 factory = null;
            DXGI.CreateDXGIFactory1<IDXGIFactory1>(out factory);
            int i = 0;

            while (factory.EnumAdapters1(i, out var adapter1).Success)
            {
                if (device != null)
                {
                    break;
                }

                int j = 0;

                while (adapter1.EnumOutputs(j, out var output).Success)
                {
                    if (device != null)
                    {
                        break;
                    }
                    if (output != null)
                    {
                        seleOutput1 = output.QueryInterface<IDXGIOutput1>();

                        var res = D3D11.D3D11CreateDevice(adapter1, DriverType.Unknown, DeviceCreationFlags.None,
                            null, out device, out var featureLevel, out var id3D11DeviceContext);
                        if (res.Success)
                        {
                            break;
                        }



                        //device = new Device(adapter1);
                        //  break;
                    }
                    j++;
                }
                i++;
            }
            var width = seleOutput1.Description.DesktopCoordinates.Right - seleOutput1.Description.DesktopCoordinates.Left;
            var height = seleOutput1.Description.DesktopCoordinates.Bottom - seleOutput1.Description.DesktopCoordinates.Top;
            var duplicatedOutput = seleOutput1.DuplicateOutput(device);

            var textureDesc = new Texture2DDescription
            {
                CPUAccessFlags = CpuAccessFlags.Read,
                BindFlags = BindFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                Width = width,
                Height = height,
                MiscFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Staging
            };
           var screenTexture = device.CreateTexture2D(textureDesc);
           orbitmap =new SKBitmap(width,height);
           bitsize = orbitmap.ByteCount;

            ImagePtr = Marshal.AllocHGlobal(width * height * 4);
            orbitmap.SetPixels(ImagePtr);
            Task.Run((() =>
            {
                while (true)
                {
                    IDXGIResource screenResource = null;
                    OutduplFrameInfo duplicateFrameInformation;
                    try
                    {
                        var res = duplicatedOutput.AcquireNextFrame(10000, out duplicateFrameInformation, out screenResource);
                        if (res.Success)
                        {
                            using (var screenTexture2D = screenResource.QueryInterface<ID3D11Texture2D>())
                            {
                                device.ImmediateContext.CopyResource(screenTexture, screenTexture2D);
                                
                                var mapSource = device.ImmediateContext.Map(screenTexture, 0, MapMode.Read, Vortice.Direct3D11.MapFlags.None);
                              

                                NativeMemory.Copy(mapSource.DataPointer.ToPointer(), ImagePtr.ToPointer(), (uint)mapSource.DepthPitch);
                                grapIndex++;
                                device.ImmediateContext.Unmap(screenTexture, 0);
                            }
                            screenResource.Dispose();
                            duplicatedOutput.ReleaseFrame();
                        }

                    }
                    catch (Exception e)
                    {

                    }
                    //Thread.Sleep(30);
                }
            }));
        }

        private unsafe void VideoGrap()
        {
           var vide= Vidoe().FirstOrDefault();
           var link = vide.GetString(CaptureDeviceAttributeKeys.SourceTypeVidcapSymbolicLink);
           var reader = CreateVideoReader(link, 1280, 720, 30);
           var typ = reader.Item2.Item1.Get<Guid>(MediaTypeAttributeKeys.Subtype);
           var transform = CreateTransform(typ, VideoFormatGuids.Argb32, reader.Item2.Item3, reader.Item2.Item4,
               reader.Item2.Item2);
           orbitmap = new SKBitmap((int)reader.Item2.Item3, (int)reader.Item2.Item4);
           bitsize = orbitmap.ByteCount;

           ImagePtr = Marshal.AllocHGlobal((int)reader.Item2.Item3 * (int)reader.Item2.Item4 * 4);
           orbitmap.SetPixels(ImagePtr);
           var stried = (int)reader.Item2.Item3 * 4;
            Task.Run((() =>
           {
               var streamInfo = transform.GetOutputStreamInfo(0);
               var _transformOutbuff = new OutputDataBuffer();
               var outpBuffer = MediaFactory.MFCreateMemoryBuffer(streamInfo.Size);
               var outpSample = MediaFactory.MFCreateSample();
               outpSample.AddBuffer(outpBuffer);
               _transformOutbuff.Sample = outpSample;
               while (true)
               {
                   try
                   {
                       reader.Item1.ReadSample(0, 0, out var index, out var flags, out var timestamp, out var sample);
                       if (sample != null)
                       {
                           transform.ProcessInput(0, sample, 0);
                           Result res = new Result();
                           do
                           {
                               res = transform.ProcessOutput(ProcessOutputFlags.None, sample.BufferCount, ref _transformOutbuff,
                                   out var status);
                               if (res.Success)
                               {
                                   var buff = _transformOutbuff.Sample.GetBufferByIndex(0);
                                   buff.Lock(out var ppbBuffer, out var pMaxLength, out var pCurrentLength);
                                   IntPtr dst = ImagePtr + stried * (int)(reader.Item2.Item4 - 1);
                                   IntPtr sour = ppbBuffer;
                                   for (int i = 0; i < reader.Item2.Item4; ++i)
                                   {
                                       NativeMemory.Copy( sour.ToPointer(), dst.ToPointer(),(uint)stried);
                                       dst -= stried;
                                       sour += stried;
                                   }
                                   buff.Unlock();
                                   buff.Release();
                                   grapIndex++;
                               }

                           } while (res.Success);
                           sample.Release();
                       }
                   }
                   catch (Exception e)
                   {
                       
                   }
               }
               outpBuffer.Dispose();
               outpSample.Dispose();
               transform.Dispose();
               reader.Item1.Dispose();
           }));
        }

        private IMFActivateCollection Vidoe()
        {
           var  config = MediaFactory.MFCreateAttributes(1);
            config.Set(CaptureDeviceAttributeKeys.SourceType, CaptureDeviceAttributeKeys.SourceTypeVidcap);
           var videos = MediaFactory.MFEnumDeviceSources(config);
           return videos;
        }
        public  (IMFSourceReader, Tuple<IMFMediaType, uint, uint, uint>) CreateVideoReader(string link, uint defaultWidth, uint defaultHeight, uint defaultFrameRate)
        {
            var config = MediaFactory.MFCreateAttributes(2);
            config.Set(CaptureDeviceAttributeKeys.SourceType, CaptureDeviceAttributeKeys.SourceTypeVidcap);
            config.Set(CaptureDeviceAttributeKeys.SourceTypeVidcapSymbolicLink, link);
            var res = MediaFactory.MFCreateDeviceSource(config, out var mediaSource);
            var des = mediaSource.CreatePresentationDescriptor();
            IMFMediaType first = null;
            Tuple<IMFMediaType, uint, uint, uint> select = null;
            for (int i = 0; i < des.StreamDescriptorCount; i++)
            {
                des.GetStreamDescriptorByIndex(i, out var selected, out var descriptor);
                var mediaHandler = descriptor.MediaTypeHandler;
                List<Tuple<IMFMediaType, uint, uint, uint>> medias = new List<Tuple<IMFMediaType, uint, uint, uint>>();
                //找到尺寸最大的
                for (int j = 0; j < mediaHandler.MediaTypeCount; j++)
                {
                    first = mediaHandler.GetMediaTypeByIndex(j);
                    MediaFactory.MFGetAttributeRatio(first, MediaTypeAttributeKeys.FrameRate, out var de, out var fenm);
                    MediaFactory.MFGetAttributeSize(first, MediaTypeAttributeKeys.FrameSize, out var width, out var height);
                    Tuple<IMFMediaType, uint, uint, uint> size =
                        new Tuple<IMFMediaType, uint, uint, uint>(first, de / fenm, width, height);
                    medias.Add(size);
                }

                var search = medias.Find(x =>
                    x.Item2 == defaultFrameRate && x.Item3 == defaultWidth && x.Item4 == defaultHeight);
                if (search == null)
                {
                    select = medias.MaxBy(x => x.Item2 * x.Item3 * x.Item4);
                    first = select?.Item1;
                }
                else
                {
                    select = search;
                    first = search.Item1;
                }

                if (select != null)
                {
                    break;
                }
                descriptor.Release();
            }

            des.Release();
            config.Release();
            if (first == null)
            {
                throw new Exception("没有可用的媒体类型");
            }
            var reader = MediaFactory.MFCreateSourceReaderFromMediaSource(mediaSource, null);
            reader.SetCurrentMediaType(0, first);
            mediaSource.Release();
            first.Release();
            return new(reader, select);
        }
        public  IMFTransform CreateTransform(Guid input, Guid outPut, uint width, uint height, uint framerate)
        {
            MediaFactory.MFStartup();
            uint flagsss = (uint)(EnumFlag.EnumFlagSyncmft | EnumFlag.EnumFlagLocalmft | EnumFlag.EnumFlagSortandfilter);
            MediaFactory.MFTEnumEx(TransformCategoryGuids.VideoProcessor, 0, new RegisterTypeInfo()
            {
                GuidMajorType = MediaTypeGuids.Video,
                GuidSubtype = input
            }, new RegisterTypeInfo()
            {
                GuidMajorType = MediaTypeGuids.Video,
                GuidSubtype = outPut
            }, out var ppp, out var pnumMftActivate);
            IMFActivate[] transform = new IMFActivate[pnumMftActivate];
            unsafe
            {
                IntPtr* numPtr = (IntPtr*)(void*)ppp;
                for (int index = 0; index < pnumMftActivate; ++index)
                    transform[index] = new IMFActivate(numPtr[index]);
                Marshal.FreeCoTaskMem(ppp);
            }
            var trans = transform[0].ActivateObject<IMFTransform>();
            using (var mediaTypeOut = MediaFactory.MFCreateMediaType())
            {
                mediaTypeOut.Set(MediaTypeAttributeKeys.MajorType, MediaTypeGuids.Video);
                mediaTypeOut.Set(MediaTypeAttributeKeys.Subtype, outPut);
                MediaFactory.MFSetAttributeSize(mediaTypeOut, MediaTypeAttributeKeys.FrameSize, (uint)width, (uint)height);
                MediaFactory.MFSetAttributeRatio(mediaTypeOut, MediaTypeAttributeKeys.FrameRate, (uint)framerate, 1);
                mediaTypeOut.Set(MediaTypeAttributeKeys.InterlaceMode, VideoInterlaceMode.Progressive);
                trans.SetOutputType(0, mediaTypeOut, 0);
                //var strmm=     MediaFactory.MFCreateFile(FileAccessMode.MfAccessModeReadwrite, FileOpenMode.MfOpenModeDeleteIfExist,
                //         FileFlags.None, "temp.mp4");

                //     MediaFactory.MFCreateMPEG4MediaSink(strmm, mediaTypeOut,null,out  var iMediaSink);
                //     sink= MediaFactory.MFCreateSinkWriterFromMediaSink(iMediaSink, null);
            }
            using (var mediaTypeIn = MediaFactory.MFCreateMediaType())
            {
                mediaTypeIn.Set(MediaTypeAttributeKeys.MajorType, MediaTypeGuids.Video);
                mediaTypeIn.Set(MediaTypeAttributeKeys.Subtype, input);
                MediaFactory.MFSetAttributeSize(mediaTypeIn, MediaTypeAttributeKeys.FrameSize, (uint)width, (uint)height);
                MediaFactory.MFSetAttributeRatio(mediaTypeIn, MediaTypeAttributeKeys.FrameRate, (uint)framerate, 1);
                mediaTypeIn.Set(MediaTypeAttributeKeys.InterlaceMode, VideoInterlaceMode.Progressive);
                trans.SetInputType(0, mediaTypeIn, 0);
            }

            return trans;
        }
        public unsafe override void OnRender(ImmediateDrawingContext drawingContext)
        {
          
            var size = this.EffectiveSize;
            //直接获取canvas，因为是私有，，只能这样拿
            var feature = drawingContext.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (feature == null)
            {
                return;
            }
            using (var lease = feature.Lease())
            {
                
                var canvas = lease.SkCanvas;
                //canvas.Clear(new SKColor(
                //    1,
                //    0,
                //    0,
                //    1
                //));
                //canvas.Save();
               // var rec = new SKRect(canvas.LocalClipBounds.Width, canvas.LocalClipBounds.Height,0 , 0);
                canvas.DrawBitmap(orbitmap, canvas.LocalClipBounds);
                //canvas.Restore();
            }

            //循环执行的赋值，
            //using (var loc = bitmap.Lock())
            //{
            //    NativeMemory.Copy(bitNint.ToPointer(), loc.Address.ToPointer(), (nuint)bitsize);
            //}

            
        }

        public override void OnMessage(object message)
        {
            if (message == StartMessage)
            {
                _running = true;
                _lastServerTime = null;
                RegisterForNextAnimationFrameUpdate();
            }
            else if (message == StopMessage)
                _running = false;
        }

        public override void OnAnimationFrameUpdate()
        {
            if (_running)
            {
                if (dispIndex < grapIndex)
                {
                    dispIndex++;
                    Invalidate();
                }
                RegisterForNextAnimationFrameUpdate();
            }
        }
    }
    private void ButtonThreadSleep(object? sender, RoutedEventArgs e)
    {
        Thread.Sleep(10000);
    }

    private void ButtonStartCustomVisual(object? sender, RoutedEventArgs e)
    {
        _customVisual?.SendHandlerMessage(CustomVisualHandler.StartMessage);
    }

    private void ButtonStopCustomVisual(object? sender, RoutedEventArgs e)
    {
        _customVisual?.SendHandlerMessage(CustomVisualHandler.StopMessage);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        _customImageVisual?.SendHandlerMessage(CustomImageVisualHandler.StartMessage);
    }

    private void StopImage(object? sender, RoutedEventArgs e)
    {
        _customImageVisual?.SendHandlerMessage(CustomImageVisualHandler.StopMessage);
    }
}
public class CompositionPageColorItem
{
    public Color Color { get; private set; }

    public SolidColorBrush ColorBrush
    {
        get { return new SolidColorBrush(Color); }
    }

    public String ColorHexValue
    {
        get { return Color.ToString().Substring(3).ToUpperInvariant(); }
    }

    public CompositionPageColorItem(Color color)
    {
        Color = color;
    }
}