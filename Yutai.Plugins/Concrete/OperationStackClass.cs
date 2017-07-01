using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.Plugins.Concrete
{
    public class OperationStackClass : IOperationStack
    {
        // Fields
        private IArray iarray_0 = new ArrayClass();
        private int int_0 = 0;

        // Methods
        public void Do(IOperation ioperation_0)
        {
            ioperation_0.Do();
            for (int i = this.iarray_0.Count - 1; i >= (this.int_0 + 1); i--)
            {
                this.iarray_0.Remove(i);
            }
            this.iarray_0.Add(ioperation_0);
            this.int_0 = this.iarray_0.Count - 1;
        }

        public void Redo()
        {
            if (this.int_0 < (this.iarray_0.Count - 1))
            {
                this.int_0++;
                (this.iarray_0.get_Element(this.int_0) as IOperation).Redo();
            }
        }

        public void Remove(int int_1)
        {
            this.iarray_0.Remove(int_1);
        }

        public void Reset()
        {
            this.iarray_0.RemoveAll();
            this.int_0 = 0;
        }

        public void Undo()
        {
            if (this.int_0 >= 0)
            {
                (this.iarray_0.get_Element(this.int_0) as IOperation).Undo();
                this.int_0--;
            }
        }

        // Properties
        public int Count
        {
            get { return this.iarray_0.Count; }
        }

        public IOperation this[int int_1]
        {
            get
            {
                if ((int_1 > 0) && (int_1 < this.iarray_0.Count))
                {
                    return (this.iarray_0.get_Element(int_1) as IOperation);
                }
                return null;
            }
        }

        public IOperation RedoOperation
        {
            get
            {
                if ((this.int_0 >= -1) && (this.int_0 < (this.iarray_0.Count - 1)))
                {
                    return (this.iarray_0.get_Element(this.int_0 + 1) as IOperation);
                }
                return null;
            }
        }

        IOperation IOperationStack.get_Item(int index)
        {
            return this.iarray_0.Element[index] as IOperation;
        }

        public IOperation UndoOperation
        {
            get
            {
                if ((this.int_0 >= 0) && (this.int_0 < this.iarray_0.Count))
                {
                    return (this.iarray_0.get_Element(this.int_0) as IOperation);
                }
                return null;
            }
        }
    }
}