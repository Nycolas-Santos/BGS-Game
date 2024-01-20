using Game.Core.Scripts.Data;

namespace Game.Core.Scripts
{
    public class UserInterfaceManager : Singleton<UserInterfaceManager>
    {
        public UserInterfaceData UserInterfaceData;
        public SubtitlesController SubtitlesController { get; set; }

        private void Start()
        {
            if (SubtitlesController == null) SubtitlesController = GetComponentInChildren<SubtitlesController>();
        }
    }
}
