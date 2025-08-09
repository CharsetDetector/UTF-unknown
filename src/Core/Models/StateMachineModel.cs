/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Mozilla Universal charset detector code.
 *
 * The Initial Developer of the Original Code is
 * Netscape Communications Corporation.
 * Portions created by the Initial Developer are Copyright (C) 2001
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *          Kohei TAKETA <k-tak@void.in> (Java port)
 *          Rudi Pettazzi <rudi.pettazzi@gmail.com> (C# port)
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */

using System;

namespace UtfUnknown.Core.Models;

/// <summary>
/// State machine model
/// </summary>
public abstract class StateMachineModel
{
    /// <summary>
    /// Start node
    /// </summary>
    public const int START = 0;

    /// <summary>
    /// Error node <see cref="Probers.ProbingState.NotMe"/> ?
    /// </summary>
    public const int ERROR = 1;

    /// <summary>
    /// <see cref="Probers.ProbingState.FoundIt"/> ?
    /// </summary>
    public const int ITSME = 2;

    public BitPackage classTable;
    public BitPackage stateTable;
    public int[] charLenTable;

    public string Name { get; }

    public int ClassFactor { get; }

    public StateMachineModel(BitPackage classTable, int classFactor,
        BitPackage stateTable, int[] charLenTable, String name)
    {
        this.classTable = classTable;
        ClassFactor = classFactor;
        this.stateTable = stateTable;
        this.charLenTable = charLenTable;
        Name = name;
    }

    public int GetClass(byte b)
    {
        return classTable.Unpack((int)b);
    }
}