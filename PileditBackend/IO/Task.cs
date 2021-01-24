using PileditBackend.TL;

namespace PileditBackend
{
    public class EditTask
    {
        public TimelineObject Type { get; set; }

        public FrameInfo Frame { get; }

        public PositionInfo Position { get; }
        public string Content { get; set; }

        public EditTask(TimelineObject type, string cont)
        {
            Type = type;
            Content = cont;
        }
    }
}
