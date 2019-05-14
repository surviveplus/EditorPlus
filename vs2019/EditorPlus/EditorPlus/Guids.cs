// Guids.cs
// MUST match guids.h
using System;

namespace Net.Surviveplus.EditorPlus
{
    static class GuidList
    {
        public const string guidEditorPlusPkgString = "69824d90-0b52-44a8-8ca3-9a991b6d2d0b";
        public const string guidEditorPlusCmdSetString = "e9a79c0a-922b-4f62-8730-ac225f6f694b";
        public const string guidToolWindowPersistanceString = "1ef19468-e77c-4227-b2e6-a014c4a9b1fa";

        public static readonly Guid guidEditorPlusCmdSet = new Guid(guidEditorPlusCmdSetString);
    };
}