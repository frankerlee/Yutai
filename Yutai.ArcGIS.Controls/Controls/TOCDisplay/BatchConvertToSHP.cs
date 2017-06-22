using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.TOCDisplay
{
    public class BatchConvertToSHP
    {
        private frmProgressBar1 frm;
        private int m_count = 0;
        private int m_nIndex = 0;
        private string m_str = "";

        private void BatchConvertToSHP_ProgressMessage(object sender, string Message)
        {
            this.frm.Text = Message;
            Application.DoEvents();
        }

        private void BatchConvertToSHP_Step()
        {
            this.m_nIndex++;
            this.frm.progressBar1.Increment(1);
            this.frm.Caption1.Text = string.Format("处理图层<{0}> ,转换第{1}/{2}个对象...", this.m_str, this.m_nIndex, this.m_count);
            Application.DoEvents();
        }

        private List<IDataset> GetAllDataset(IMap pMap)
        {
            List<IDataset> list = new List<IDataset>();
            UID uid = new UIDClass {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layer = pMap.get_Layers(uid, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
            {
                IFeatureLayer layer3 = layer2 as IFeatureLayer;
                if ((layer3 != null) && (layer3.FeatureClass != null))
                {
                    list.Add(layer3.FeatureClass as IDataset);
                }
            }
            return list;
        }

        private void p_SetFeatureClassNameEnvent(string str)
        {
            string[] strArray = str.Split(new char[] { '.' });
            str = strArray[strArray.Length - 1];
            if (str.Length > 36)
            {
                str = str.Substring(0, str.Length - 36);
            }
            this.m_str = str;
            this.frm.Text = "处理:" + str;
        }

        private void p_SetFeatureCountEnvent(int i)
        {
            this.m_count = i;
            this.frm.progressBar1.Maximum = i;
        }

        public void ToShape(IMap pMap)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                List<IDataset> allDataset = this.GetAllDataset(pMap);
                Dataloaders dataloaders = new Dataloaders();
                IPropertySet set = new PropertySetClass();
                set.SetProperty("DATABASE", dialog.SelectedPath);
                IWorkspaceName name = new WorkspaceNameClass {
                    ConnectionProperties = set,
                    WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory"
                };
                frmProgressBar1 bar = new frmProgressBar1 {
                    progressBar1 = { Maximum = allDataset.Count, Value = 0 }
                };
                bar.Show();
                bar.Text = "导出shapefile";
                foreach (IDataset dataset in allDataset)
                {
                    string[] strArray = dataset.Name.Split(new char[] { '.' });
                    strArray = strArray[strArray.Length - 1].Split(new char[] { '_' });
                    bar.Caption1.Text = string.Format("导出[{0}]", strArray[0]);
                    Application.DoEvents();
                    bar.progressBar1.Value++;
                    try
                    {
                        dataloaders.ConvertData(dataset.FullName as IDatasetName, name as IName, strArray[0], null);
                    }
                    catch
                    {
                    }
                }
                bar.Close();
                MessageBox.Show("导出完成!");
            }
        }

        public void ToVCT(IMap pMap)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "VCT文件(*.vct)|*.vct"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                List<IDataset> allDataset = this.GetAllDataset(pMap);
                if (allDataset.Count > 0)
                {
                    this.frm = new frmProgressBar1();
                    this.frm.progressBar1.Maximum = allDataset.Count;
                    this.frm.progressBar1.Value = 0;
                    this.frm.Show();
                    this.frm.Text = "导出VCT";
                    VCTWrite write = new VCTWrite();
                    write.ProgressMessage += new ProgressMessageHandle(this.BatchConvertToSHP_ProgressMessage);
                    write.Step+=(new ESRI.ArcGIS.Geodatabase.IFeatureProgress_StepEventHandler(this.BatchConvertToSHP_Step));
                    write.SetFeatureClassNameEnvent += new SetFeatureClassNameEnventHandler(this.p_SetFeatureClassNameEnvent);
                    write.SetFeatureCountEnvent += new SetFeatureCountEnventHandler(this.p_SetFeatureCountEnvent);
                    for (int i = 0; i < allDataset.Count; i++)
                    {
                        write.AddDataset(allDataset[i]);
                    }
                    write.Write(dialog.FileName);
                    this.frm.Close();
                    MessageBox.Show("导出完成!");
                }
            }
        }
    }
}

