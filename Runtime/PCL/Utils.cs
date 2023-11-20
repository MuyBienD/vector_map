﻿#region License
/******************************************************************************
* Copyright 2018-2020 The AutoCore Authors. All Rights Reserved.
* 
* Licensed under the GNU Lesser General Public License, Version 3.0 (the "License"); 
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
* https://www.gnu.org/licenses/lgpl-3.0.html
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*****************************************************************************/
#endregion


using Unity.Collections;

namespace AutoCore.MapToolbox.PCL
{
    static class Utils
    {
        public static void FillNativeArray<T>(this NativeArray<T> array, T data) where T : struct
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = data;
            }
        }
    }
}