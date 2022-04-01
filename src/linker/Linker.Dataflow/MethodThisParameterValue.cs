// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ILLink.Shared.DataFlow;
using Mono.Cecil;
using Mono.Linker;
using Mono.Linker.Dataflow;
using TypeDefinition = Mono.Cecil.TypeDefinition;


namespace ILLink.Shared.TrimAnalysis
{

	/// <summary>
	/// A value that came from the implicit this parameter of a method
	/// </summary>
	partial record MethodThisParameterValue : IValueWithStaticType
	{
		public MethodThisParameterValue (MethodDefinition method, DynamicallyAccessedMemberTypes dynamicallyAccessedMemberTypes)
		{
			Method = method;
			DynamicallyAccessedMemberTypes = dynamicallyAccessedMemberTypes;
		}

		public readonly MethodDefinition Method;

		public override DynamicallyAccessedMemberTypes DynamicallyAccessedMemberTypes { get; }

		public override IEnumerable<string> GetDiagnosticArgumentsForAnnotationMismatch ()
			=> new string[] { Method.GetDisplayName () };

		public TypeDefinition? StaticType => Method.DeclaringType;

		public override SingleValue DeepCopy () => this; // This value is immutable

		public override string ToString () => this.ValueToString (Method, DynamicallyAccessedMemberTypes);
	}
}