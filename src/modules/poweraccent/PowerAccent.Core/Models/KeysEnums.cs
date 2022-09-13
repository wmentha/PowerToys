// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Vanara.PInvoke;

namespace PowerAccent.Core;

public enum LetterKey
{
    A = User32.VK.VK_A,
    B = User32.VK.VK_B,
    C = User32.VK.VK_C,
    D = User32.VK.VK_D,
    E = User32.VK.VK_E,
    F = User32.VK.VK_F,
    G = User32.VK.VK_G,
    H = User32.VK.VK_H,
    I = User32.VK.VK_I,
    J = User32.VK.VK_J,
    K = User32.VK.VK_K,
    L = User32.VK.VK_L,
    M = User32.VK.VK_M,
    N = User32.VK.VK_N,
    O = User32.VK.VK_O,
    P = User32.VK.VK_P,
    Q = User32.VK.VK_Q,
    R = User32.VK.VK_R,
    S = User32.VK.VK_S,
    T = User32.VK.VK_T,
    U = User32.VK.VK_U,
    V = User32.VK.VK_V,
    W = User32.VK.VK_W,
    X = User32.VK.VK_X,
    Y = User32.VK.VK_Y,
    Z = User32.VK.VK_Z,
    Currency = 0x34,
    Exclamation = 0x31,
    Question = User32.VK.VK_OEM_2, // 0xBF
}

public enum TriggerKey
{
    Left = User32.VK.VK_LEFT,
    Right = User32.VK.VK_RIGHT,
    Space = User32.VK.VK_SPACE,
}
