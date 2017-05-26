﻿using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Services
{
    public interface IProjectService
    {
        bool IsEmpty { get; }
        string Filename { get; }
        ProjectState GetState();
        bool TryClose();
        bool Save();
        bool SaveAs();
        bool Open();
        bool Open(string filename, bool silent = true);
        void SetModified();
        bool Modified { get; }
    }
}