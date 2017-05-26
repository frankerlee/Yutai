using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Plugins.Mvp
{
    
    public interface IViewInternal
    {
        event Action OkClicked;

        ViewStyle Style { get; }

        bool Visible { get; }

        void BeforeClose();

        void Close();

        void ShowView(IWin32Window parent = null);

        void UpdateView();
    }

    public interface IViewInternal<TModel> : IViewInternal
    {
        TModel Model { get; }

        void InitInternal(TModel model);
    }

    public class ViewStyle
    {
        public bool Modal { get; set; }
        public bool Sizable { get; set; }

        public ViewStyle(bool sizable)
        {
            Modal = true;
            Sizable = sizable;
        }

        public ViewStyle()
        {

        }
    }
}
