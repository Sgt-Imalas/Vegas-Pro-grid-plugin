using ScriptPortal.Vegas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridPlugin
{
    public class EntryPoint
    {        
        public void FromVegas(Vegas MyVegas)
        {
            int rows;
            int columns;
            int startX;
            int startY;
            bool resetOnly = false;


            using (Form1 form1 = new Form1())
            {
                if (form1.ShowDialog() != DialogResult.OK)
                    return;
                rows = form1.Rows;
                columns = form1.Columns;
                startX = form1.startX;
                startY = form1.startY;
                resetOnly = form1.resetOnly;

            }
            this.ArrangeGrid(MyVegas.Project , rows, columns, startX, startY,resetOnly);
        }
        private void ArrangeGrid(Project p, int rows, int columns, int startX, int startY, bool resetOnly)
        {
            List<VideoEvent> selectedVideoEvents = this.GetSelectedVideoEvents(p);
            int num1 = startX - 1;
            int num2 = startY - 1;
            if (selectedVideoEvents.Count() > rows * columns)
            {
                MessageBox.Show("Too many video tracks selected.\nPlease increase the row/column count or\n deselect " + (selectedVideoEvents.Count() - rows * columns) + " video tracks!");
            }
            else
            {
                foreach (var current in selectedVideoEvents)
                {
                    foreach (var i in current.VideoMotion.Keyframes)
                    {
                        current.VideoMotion.Keyframes.Remove(i);
                    }

                    VideoMotionKeyframe videoMotionKeyframe = new VideoMotionKeyframe(Timecode.FromFrames(0L));
                    current.VideoMotion.Keyframes.Add(videoMotionKeyframe);                                   
                    
                    if (!resetOnly)
                    {
                        float x = current.VideoMotion.Keyframes.First().BottomRight.X;
                        float y = current.VideoMotion.Keyframes.First().BottomRight.Y;
                        videoMotionKeyframe.Bounds =
                            new VideoMotionBounds(
                                (float)(0.0 - (double)num1 * (double)x),
                                (float)(0.0 - (double)num2 * (double)y),

                                (float)((double)columns * (double)x - (double)num1 * (double)x),
                                (float)(0.0 - (double)num2 * (double)y),

                                (float)((double)columns * (double)x - (double)num1 * (double)x),
                                (float)((double)rows * (double)y - (double)num2 * (double)y),

                                (float)(0.0 - (double)num1 * (double)x),
                                (float)((double)rows * (double)y - (double)num2 * (double)y));

                            
                        if (num1 < columns - 1)
                        {
                            ++num1;
                        }
                        else
                        {
                            num1 = 0;
                            ++num2;
                        }                        
                    }
                }
            }
        }

        private List<VideoEvent> GetSelectedVideoEvents(Project p)
        {
            List<VideoEvent> videoEventList = new List<VideoEvent>();
            using (IEnumerator<Track> enumerator1 = ((BaseList<Track>)p.Tracks).GetEnumerator())
            {
                while (((IEnumerator)enumerator1).MoveNext())
                {
                    using (IEnumerator<TrackEvent> enumerator2 = ((BaseList<TrackEvent>)enumerator1.Current.Events).GetEnumerator())
                    {
                        while (((IEnumerator)enumerator2).MoveNext())
                        {
                            TrackEvent current = enumerator2.Current;
                            if (current.MediaType == MediaType.Video && current.Selected)
                                videoEventList.Add((VideoEvent)current);
                        }
                    }
                }
            }
            return videoEventList;
        }
    }
}

