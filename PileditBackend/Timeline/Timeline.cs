using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PileditBackend.TL
{
    public class Timeline
    {
        public TimelineObject this[ushort layer, uint frame]
        {
            get => GetObject(layer, frame);
        }
        public TimelineObject this[ushort layer, FrameInfo frame]
        {
            get => GetObject(layer, frame);
        }
        private IReadOnlyDictionary<FrameInfo, TimelineObject>[] Objects;
        public int LayerCount { get => Objects.Length; }

        public Timeline()
        {
            Objects = new Dictionary<FrameInfo, TimelineObject>[10];
        }

        public void AddObject(ushort layer, FrameInfo frame, TimelineObject obj)
        {
            var objs = Objects;
            //if (LayerCount < layer) Array.Resize(ref objs, LayerCount);
            //else if (objs[layer].ContainsKey(frame)) return;
            objs[layer] = new Dictionary<FrameInfo, TimelineObject>(objs[layer]) { { frame, obj } };
            Objects = objs;
        }
        public TimelineObject GetObject(ushort layer, uint frame)
        {
            return GetObject(layer, (FrameInfo)frame);
        }
        public TimelineObject GetObject(ushort layer, FrameInfo frame)
        {
            var dic = Objects[layer];
            return dic.ContainsKey(frame) ? dic[layer] : null;
        }

        public Mat GetMat(uint frame)
        {
            Mat mat = null;
            for(int i = 0;i < LayerCount; i++)
            {
                var obj = GetObject((ushort)i, frame);
                Mat src;
                if (obj == null) continue;
                else if (obj is TimelinePrintObject tpo)
                {
                    var fi = Objects[i].FirstOrDefault(c => c.Value == obj).Key;
                    if (mat == null)
                    {
                        mat = tpo.GetMat(frame - fi.Begin);
                        continue;
                    }
                    src = tpo.GetMat(frame - fi.Begin);
                    if (mat == null) mat = src;
                    else
                    {
                        Mat gray = new Mat(), mask = new Mat(), res = new Mat(), nm = new Mat();
                        Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                        Cv2.BitwiseNot(gray, mask);
                        Cv2.BitwiseOr(src, src, res, mask);
                        Cv2.BitwiseOr(mat, res, nm);
                        mat = nm;
                    }
                }
                else if (obj is TimelineComponent tc)
                {
                    src = tc.GetMat(frame);
                    if (src == null)
                    {
                        if (mat == null) continue;
                        else src = mat.Clone();
                    }

                    foreach (var e in tc.Effects) e.Processing(src);
                }
                else continue;

                if (mat == null) mat = src;
                else
                {
                    Mat gray = new Mat(), mask = new Mat(), res = new Mat(), nm = new Mat();
                    Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                    Cv2.BitwiseNot(gray, mask);
                    Cv2.BitwiseOr(src, src, res, mask);
                    Cv2.BitwiseOr(mat, res, nm);
                    mat = nm;
                }
            }
            return mat;
        }
        public Mat GetMat(ushort layer, uint frame)
        {
            Mat mat = null;
            var obj = GetObject(layer, frame);
            if (obj != null && obj is TimelinePrintObject)
            {
                var tpo = obj as TimelinePrintObject;
                var fi = Objects[layer].FirstOrDefault(c => c.Value == obj).Key;
                mat = tpo.GetMat(frame - fi.Begin);
            }
            return mat;
        }
    }
}