using System;
using System.Collections;

namespace AcornPad.Internal
{
    public class UndoRedo
    {
        /// <summary>
        ///
        /// </summary>
        private readonly Stack undo = new Stack();

        /// <summary>
        ///
        /// </summary>
        private readonly Stack redo = new Stack();

        /// <summary>
        ///
        /// </summary>
        public bool CanUndo => undo.Count > 0;

        /// <summary>
        ///
        /// </summary>
        public bool CanRedo => redo.Count > 0;

        /// <summary>
        ///
        /// </summary>
        public UndoRedo()
        {
            Clear();
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear()
        {
            undo.Clear();
            redo.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        public void AddHistory(Snapshot current)
        {
            undo.Push(current);
            redo.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public object Undo(Snapshot current)
        {
            object obj = null;

            if (CanUndo)
            {
                redo.Push(current);
                obj = undo.Pop();
            }

            return obj;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public object UndoPeek()
        {
            object obj = null;

            if (undo.Count > 0)
            {
                obj = undo.Peek();
            }
            return obj;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public object Redo(Snapshot current)
        {
            object obj = null;

            if (CanRedo)
            {
                undo.Push(current);
                obj = redo.Pop();
            }

            return obj;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public object RedoPeek()
        {
            object obj = null;

            if (redo.Count > 0)
            {
                obj = redo.Peek();
            }

            return obj;
        }

        /// <summary>
        ///  Simple test - need to remove once live
        /// </summary>
        /// <returns></returns>
        public string UndoList()
        {
            string jsonString = string.Empty;

            for (int i = 0; i < undo.Count; i++)
            {
                jsonString += string.Format("{0} - {1}", (i + 1).ToString().PadLeft(4, ' '), ((Snapshot)undo.ToArray()[i]).Description);
                jsonString += Environment.NewLine;
            }

            //jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(undo, Newtonsoft.Json.Formatting.Indented);
            return jsonString;// != string.Empty ? jsonString : string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string RedoList()
        {
            //string jsonString;
            //jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(redo, Newtonsoft.Json.Formatting.Indented);
            //return jsonString != string.Empty ? jsonString : string.Empty;

            string jsonString = string.Empty;

            for (int i = 0; i < redo.Count; i++)
            {
                jsonString += string.Format("{0} - {1}", (i + 1).ToString().PadLeft(4, ' '), ((Snapshot)redo.ToArray()[i]).Description);
                jsonString += Environment.NewLine;
            }

            return jsonString;
        }
    }
}