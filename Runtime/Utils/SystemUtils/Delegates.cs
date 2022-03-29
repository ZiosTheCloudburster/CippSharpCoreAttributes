namespace CippSharp.Core
{
    /// <summary>
    /// Same as System.Action'T' but with 'ref'
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void RefAction<T>(ref T element);

    /// <summary>
    /// Sames as System.Action'T' but with 'ref' and an index parameter
    /// </summary>
    /// <param name="element"></param>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    public delegate void ForRefAction<T>(ref T element, int index);
}
