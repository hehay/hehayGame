

namespace komal.puremvc
{
    public class SceneBase : ComponentEx
    {
        public static SceneBase Instance;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        
            DontDestroyOnLoad(this);
        }

        protected virtual void Start()
        {

        }

        void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {

            }
            else
            {

            }
        }

        void OnApplicationQuit()
        {

        }

        public virtual void Update()
        {

        }
    }
}