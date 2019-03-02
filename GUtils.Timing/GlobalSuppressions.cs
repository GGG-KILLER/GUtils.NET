﻿/*
 * Copyright © 2019 GGG KILLER <gggkiller2@gmail.com>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the “Software”), to deal in the Software without
 * restriction, including without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
 * the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project. Project-level
// suppressions either have no target or are given a specific
// target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage ( "Naming", "CC0029:Disposables Should Call Suppress Finalize", Justification = "This isn't a proper IDisposable implementation", Scope = "member", Target = "~M:GUtils.Timing.TimingLine.Dispose" )]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage ( "Naming", "CC0029:Disposables Should Call Suppress Finalize", Justification = "This isn't a proper IDisposable implementation", Scope = "member", Target = "~M:GUtils.Timing.TimingArea.Dispose" )]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage ( "Style", "CC0013:Use ternary operator", Justification = "Code would get messy", Scope = "member", Target = "~M:GUtils.Timing.Timespans.Format(System.Int64,System.String)~System.String" )]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage ( "Style", "CC0013:Use ternary operator", Justification = "Code would get messy", Scope = "member", Target = "~M:GUtils.Timing.Duration.Format(System.Int64,System.String)~System.String" )]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage ( "Style", "IDE0046:Convert to conditional expression", Justification = "Code would get messy", Scope = "member", Target = "~M:GUtils.Timing.Duration.Format(System.Int64,System.String)~System.String" )]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage ( "Design", "CA1063:Implement IDisposable Correctly", Justification = "There is no correct implementation of IDisposable abuse", Scope = "type", Target = "~T:GUtils.Timing.TimingArea" )]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage ( "Design", "CA1063:Implement IDisposable Correctly", Justification = "There is no correct implementation of IDisposable abuse", Scope = "type", Target = "~T:GUtils.Timing.TimingLine" )]

