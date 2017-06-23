using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;

namespace Yutai.Test.Commands
{
    public class CmdTest : YutaiCommand
    {
        public CmdTest(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "测试";
            base.m_category = "Test";
            base.m_name = "Test_Common_Test";
            base._key = "Test_Common_Test";
            base.m_toolTip = "测试";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            string xmlFieldName = SelectXmlDoc();
            if (string.IsNullOrWhiteSpace(xmlFieldName))
                return;
            IPipelineConfig config = new PipelineConfig();
            config.LoadFromXml(xmlFieldName);
            string saveXmlFile = SaveFile();
            config.SaveToXml(saveXmlFile);
            MessageBox.Show(@"执行完成");
        }

        private string SaveFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = @"XML文档(*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return null;
        }

        private string SelectXmlDoc()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = @"XML文档(*.xml)|*.xml";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return null;
        }
    }
}
