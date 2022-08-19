using System.Threading.Tasks;

namespace GATVirtualBooth
{
    public class PopUpNotice : BasePopUp, IHideShow
    {

        private new void Awake()
        {
            base.Awake();
        }

        public Task<int> Show(string msg, string btn0)
        {
            Show();

            messageText.text = msg;
            button0Text.text = btn0;

            tcs = new TaskCompletionSource<int>();
            return tcs.Task;
        }
    }
}
