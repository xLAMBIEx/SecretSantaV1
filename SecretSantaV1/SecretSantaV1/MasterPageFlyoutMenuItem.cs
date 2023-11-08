using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSantaV1
{

    public class MasterPageFlyoutMenuItem
    {
        public MasterPageFlyoutMenuItem()
        {
            TargetType = typeof(MasterPageFlyoutMenuItem);
            SubMenuItems = new List<SubMenuItem>();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public List<SubMenuItem> SubMenuItems { get; set; }

        public Type TargetType { get; set; }
    }

    public class SubMenuItem
    {
        public SubMenuItem()
        {
            TargetType = typeof(SubMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}