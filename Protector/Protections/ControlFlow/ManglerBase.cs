﻿using dnlib.DotNet;
using dnlib.DotNet.Emit;
using OliviaGuard.Protector.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OliviaGuard.Protector.Protections.ControlFlow.BlockParser;

namespace OliviaGuard.Protector.Protections.ControlFlow
{
    internal abstract class ManglerBase
    {
        protected static IEnumerable<InstrBlock> GetAllBlocks(ScopeBlock scope)
        {
            foreach (BlockBase child in scope.Children)
            {
                if (child is InstrBlock)
                    yield return (InstrBlock)child;
                else
                {
                    foreach (InstrBlock block in GetAllBlocks((ScopeBlock)child))
                        yield return block;
                }
            }
        }

        public abstract void Mangle(CilBody body, ScopeBlock root, OliviaLib ctx, MethodDef method, TypeSig retType);
    }

}
