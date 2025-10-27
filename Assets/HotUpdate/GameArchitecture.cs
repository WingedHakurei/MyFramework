using HotUpdate.Model.Player;
using HotUpdate.Model.Store;
using HotUpdate.Utility;
using QFramework;

namespace HotUpdate
{
    public class GameArchitecture : Architecture<GameArchitecture>
    {
        protected override void Init()
        {
            RegisterUtility(new DataTableUtility());
            RegisterModel(new InventoryModel());
            RegisterModel(new StoreModel());
        }
    }
}