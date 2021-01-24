using PileditBackend.Effects;
using PileditBackend.TL;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using static OpenCvSharp.VideoCaptureProperties;
using static System.StringComparison;

namespace PileditBackend.IO
{
    public static class Movie
    {
        public static void OutputMovie(string path, string extention, FourCC cc,
            VideoCapture cap, Dictionary<FrameInfo, PrintEffectBase> effect = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (extention == null) throw new ArgumentNullException(nameof(extention));
            if (cap == null) throw new ArgumentNullException(nameof(cap));

            if (extention[0] != '.') extention = "." + extention;
            if (!path.EndsWith(extention, CurrentCulture)) path += extention;
            Size size = new Size(cap.Get(FrameWidth), cap.Get(FrameHeight));
            double fps = cap.Get(Fps);
            using VideoWriter vw = new VideoWriter(path, cc, fps, size);
            Mat frame;
            cap.Set(PosFrames, 0);
            do
            {
                frame = cap.RetrieveMat();
                var f = (uint)cap.Get(PosFrames);
                if (effect != null)
                {
                    foreach (var eff in effect)
                    {
                        if (eff.Key.Begin <= f && f <= eff.Key.End) frame = eff.Value.Processing(frame);
                    }
                }
                vw.Write(frame);
                Log.Progress("Outputing Movie", f / cap.Get(FrameCount) * 100);
            }
            while (!frame.Empty());
        }

        public static void OutputMovie(string path, ExtentionBase extention,
            VideoCapture cap = null, Dictionary<FrameInfo, PrintEffectBase> effect = null)
        {
            if (extention.CC is FourCC cc) OutputMovie(path, extention.Name, cc, cap, effect);
            else Log.Error("対応していないコーデックです");
        }

        public static void OutputMovie(string path, string extention, FourCC cc, Project project)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (extention == null) throw new ArgumentNullException(nameof(extention));
            if (project == null) throw new ArgumentNullException(nameof(project));

            if (extention[0] != '.') extention = "." + extention;
            if (!path.EndsWith(extention, CurrentCulture)) path += extention;
            var size = project.OutputSize;
            var tl = project.Timeline;
            double fps = 60;//cap.Get(Fps);
            var framec = 10;
            using VideoWriter vw = new VideoWriter(path, cc, fps, size);
            Mat frame;
            for(uint i = 0; i < framec; i++)
            {
                frame = tl.GetMat(i);
                vw.Write(frame);
                Log.Progress("Outputing Movie", (i + 1) / framec * 100);
            }
        }
    }
}
