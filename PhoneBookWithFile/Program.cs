using PhoneBookWithFile.Services;

namespace PhoneBookWithFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuService menuService = new MenuService();
            menuService.ShowMenuService();
        }
    }
}