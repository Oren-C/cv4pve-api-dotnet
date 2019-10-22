﻿/*
 * This file is part of the cv4pve-api-dotnet https://github.com/Corsinvest/cv4pve-api-dotnet,
 *
 * This source file is available under two different licenses:
 * - GNU General Public License version 3 (GPLv3)
 * - Corsinvest Enterprise License (CEL)
 * Full copyright and license information is available in
 * LICENSE.md which is distributed with this source code.
 *
 * Copyright (C) 2016 Corsinvest Srl	GPLv3 and CEL
 */
 
using System.Linq;

namespace Corsinvest.ProxmoxVE.Api.Extension.VM
{
    /// <summary>
    /// Disk Qemu
    /// </summary>
    public class DiskQemu : Disk
    {
        internal DiskQemu(PveClient client, string id, string definition) : base(client, id, definition)
        {
            var backup = definition.Split(':')[1].Split(',').Where(a => a.StartsWith("backup=")).FirstOrDefault();
            Backup = backup == null ? true : backup == "1";
        }
    }
}