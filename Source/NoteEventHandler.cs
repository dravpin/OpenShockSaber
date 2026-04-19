using SiraUtil.Affinity;
using Zenject;

namespace OpenShockSaber
{
    public class NoteEventHandler : IAffinity
    {
        private readonly OpenShockManager _shocker;

        public NoteEventHandler(OpenShockManager shocker)
        {
            _shocker = shocker;
        }

        [AffinityPostfix]
        [AffinityPatch(typeof(BeatmapObjectManager), "HandleNoteWasMissed")]
        internal void OnNoteMissed(NoteController noteController)
        {
            if (noteController.noteData.colorType != ColorType.None)
                _shocker.RequestShock();
        }

        [AffinityPostfix]
        [AffinityPatch(typeof(BeatmapObjectManager), "HandleNoteWasCut")]
        internal void OnNoteCut(NoteController noteController, in NoteCutInfo noteCutInfo)
        {
            if (noteController.noteData.colorType == ColorType.None) return;

            if (!noteCutInfo.allIsOld && OpenShockSaberConfig.Instance.ShockOnBadCut)
            {
                _shocker.RequestShock();
                return;
            }

            ScoreModel.RawScoreWithoutMultiplier(noteCutInfo, out int before, out int after, out int distance);
            if ((before + after + distance) < OpenShockSaberConfig.Instance.MinScoreThreshold)
            {
                _shocker.RequestShock();
            }
        }
    }
}