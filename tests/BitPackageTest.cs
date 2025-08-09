﻿/* ***** BEGIN LICENSE BLOCK *****
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

using NUnit.Framework;
using UtfUnknown.Core;

namespace UtfUnknown.Tests;

public class BitPackageTest
{
    [Test]
    public void TestPack()
    {
        Assert.AreEqual(BitPackage.Pack4bits(0,0,0,0,0,0,0,0), 0);
        Assert.AreEqual(BitPackage.Pack4bits(1,1,1,1,1,1,1,1), 286331153);
        Assert.AreEqual(BitPackage.Pack4bits(2,2,2,2,2,2,2,2), 572662306);
        Assert.AreEqual(BitPackage.Pack4bits(15,15,15,15,15,15,15,15), -1);
    }

    [Test]
    public void TestUnpack()
    {
        int[] data = new int[] {
            BitPackage.Pack4bits(0, 1, 2, 3, 4, 5, 6, 7),
            BitPackage.Pack4bits(8, 9, 10, 11, 12, 13, 14, 15)
        };

        BitPackage pkg = new BitPackage(
            BitPackage.INDEX_SHIFT_4BITS,
            BitPackage.SHIFT_MASK_4BITS,
            BitPackage.BIT_SHIFT_4BITS,
            BitPackage.UNIT_MASK_4BITS,
            data);

        for (int i = 0; i < 16; i++) {
            int n = pkg.Unpack(i);
            Assert.AreEqual(n, i);
        }
    }
}
