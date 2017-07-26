using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcSpatialReference : UserControl
    {
        public UcSpatialReference()
        {
            InitializeComponent();
        }

        public EnumSpatialReferenceType ReferenceType
        {
            get
            {
                switch ((int)rgType.EditValue)
                {
                    case 0:
                        return EnumSpatialReferenceType.ClassReference;
                    case 1:
                        return EnumSpatialReferenceType.MapReference;
                    case 2:
                        return EnumSpatialReferenceType.DatasetReference;
                    default:
                        return EnumSpatialReferenceType.ClassReference;
                }
            }
        }
    }

    public enum EnumSpatialReferenceType
    {
        ClassReference = 0,
        MapReference = 1,
        DatasetReference = 2
    }
}
