﻿using dnlib.DotNet;
using dnlib.DotNet.Emit;
using OliviaGuard.Protector.Class;
using System;

namespace OliviaGuard.Protector.Protections
{
    public class MeltConstants
    {
        public MeltConstants(OliviaLib lib) => Main(lib);

        void Main(OliviaLib lib)
        {
            foreach (TypeDef type in lib.moduleDef.Types)
            {
                if (type.IsGlobalModuleType) continue;
                foreach (MethodDef method in type.Methods)
                {
                    //Exclude the "_My" nammespace from VB.NET files
                    if (method.FullName.Contains("My.")) continue; //VB gives cancer anyway
                    //Exclude constructor, aka .ctor method in decompiler
                    if (method.IsConstructor) continue;
                    if (!method.HasBody) continue;
                    var instr = method.Body.Instructions;
                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        //if instruction is an LDSTR (string) instruction then
                       /* if (instr[i].OpCode == OpCodes.Ldstr)
                        {
                            Random rn = new Random();
                            for (int j = 1; j < rn.Next(16); j++)
                            {
                                if (j != 1) j += 1;
                                //Create a new local 
                                Local new_local = new Local(lib.moduleDef.CorLibTypes.String);
                                //Create another new local
                                Local new_local2 = new Local(lib.moduleDef.CorLibTypes.String);
                                //add them in the method
                                method.Body.Variables.Add(new_local);
                                method.Body.Variables.Add(new_local2);
                                //set ldstr value to the local
                                instr.Insert(i + j, Instruction.Create(OpCodes.Stloc_S, new_local));
                                instr.Insert(i + (j + 1), Instruction.Create(OpCodes.Ldloc_S, new_local));
                            }
                       }
                       */ 
                        if (instr[i].OpCode == OpCodes.Ldc_I4)
                        {
                            Random rn = new Random();
                            for (int j = 1; j < rn.Next(16); j++)
                            {
                                if (j != 1) j += 1;
                                Local new_local = new Local(lib.moduleDef.CorLibTypes.Int32);
                                Local new_local2 = new Local(lib.moduleDef.CorLibTypes.Int32);
                                method.Body.Variables.Add(new_local);
                                method.Body.Variables.Add(new_local2);
                                instr.Insert(i + j, Instruction.Create(OpCodes.Stloc_S, new_local));
                                instr.Insert(i + (j + 1), Instruction.Create(OpCodes.Ldloc_S, new_local));
                            }
                        }
                    }
                }
            }
        }
    }
}