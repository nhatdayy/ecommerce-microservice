﻿using System.Reflection;

namespace Catalog.Contracts;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
