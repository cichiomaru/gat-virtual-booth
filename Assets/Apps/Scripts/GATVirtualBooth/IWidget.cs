namespace GATVirtualBooth
{
    public interface IWidget
    {
        public string Path { get; }

        public void Hide();
        public void Show();
        
    }
}