using System;

namespace Yutai.Shared
{
    public interface IEnumConverter<T> where T : IConvertible
    {
        string GetString(T value);
    }
}