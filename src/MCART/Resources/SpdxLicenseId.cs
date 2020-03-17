/*
SpdxLicenseId.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Resources
{

    /// <summary>
    /// Enumera todas las licencias registradas dentro de los estándares de
    /// Software Package Data Exchange (SPDX).
    /// </summary>
    public enum SpdxLicenseId
    {
        /// <summary>
        /// BSD Zero Clause License
        /// </summary>
        [Description("BSD Zero Clause License"), Name("0BSD")] BSD0,

        /// <summary>
        /// Attribution Assurance License
        /// </summary>
        [Description("Attribution Assurance License")] AAL,

        /// <summary>
        /// Abstyles License
        /// </summary>
        [Description("Abstyles License")] Abstyles,

        /// <summary>
        /// Adobe Systems Incorporated Source Code License Agreement
        /// </summary>
        [Description("Adobe Systems Incorporated Source Code License Agreement")] Adobe_2006,

        /// <summary>
        /// Adobe Glyph List License
        /// </summary>
        [Description("Adobe Glyph List License")] Adobe_Glyph,

        /// <summary>
        /// Amazon Digital Services License
        /// </summary>
        [Description("Amazon Digital Services License")] ADSL,

        /// <summary>
        /// Academic Free License v1.1
        /// </summary>
        [Description("Academic Free License v1.1")] AFL_1_1,

        /// <summary>
        /// Academic Free License v1.2
        /// </summary>
        [Description("Academic Free License v1.2")] AFL_1_2,

        /// <summary>
        /// Academic Free License v2.0
        /// </summary>
        [Description("Academic Free License v2.0")] AFL_2_0,

        /// <summary>
        /// Academic Free License v2.1
        /// </summary>
        [Description("Academic Free License v2.1")] AFL_2_1,

        /// <summary>
        /// Academic Free License v3.0
        /// </summary>
        [Description("Academic Free License v3.0")] AFL_3_0,

        /// <summary>
        /// Afmparse License
        /// </summary>
        [Description("Afmparse License")] Afmparse,

        /// <summary>
        /// Affero General Public License v1.0 only
        /// </summary>
        [Description("Affero General Public License v1.0 only")] AGPL_1_0_only,

        /// <summary>
        /// Affero General Public License v1.0 or later
        /// </summary>
        [Description("Affero General Public License v1.0 or later")] AGPL_1_0_or_later,

        /// <summary>
        /// GNU Affero General Public License v3.0 only
        /// </summary>
        [Description("GNU Affero General Public License v3.0 only"), LicenseUri("https://www.gnu.org/licenses/agpl-3.0.txt")] AGPL_3_0_only,

        /// <summary>
        /// GNU Affero General Public License v3.0 or later
        /// </summary>
        [Description("GNU Affero General Public License v3.0 or later"), LicenseUri("https://www.gnu.org/licenses/agpl-3.0.txt")] AGPL_3_0_or_later,

        /// <summary>
        /// Aladdin Free Public License
        /// </summary>
        [Description("Aladdin Free Public License")] Aladdin,

        /// <summary>
        /// AMD's plpa_map.c License
        /// </summary>
        [Description("AMD's plpa_map.c License")] AMDPLPA,

        /// <summary>
        /// Apple MIT License
        /// </summary>
        [Description("Apple MIT License")] AML,

        /// <summary>
        /// Academy of Motion Picture Arts and Sciences BSD
        /// </summary>
        [Description("Academy of Motion Picture Arts and Sciences BSD")] AMPAS,

        /// <summary>
        /// ANTLR Software Rights Notice
        /// </summary>
        [Description("ANTLR Software Rights Notice")] ANTLR_PD,

        /// <summary>
        /// Apache License 1.0
        /// </summary>
        [Description("Apache License 1.0")] Apache_1_0,

        /// <summary>
        /// Apache License 1.1
        /// </summary>
        [Description("Apache License 1.1")] Apache_1_1,

        /// <summary>
        /// Apache License 2.0
        /// </summary>
        [Description("Apache License 2.0"), LicenseUri("http://www.apache.org/licenses/LICENSE-2.0.txt")] Apache_2_0,

        /// <summary>
        /// Adobe Postscript AFM License
        /// </summary>
        [Description("Adobe Postscript AFM License")] APAFML,

        /// <summary>
        /// Adaptive Public License 1.0
        /// </summary>
        [Description("Adaptive Public License 1.0")] APL_1_0,

        /// <summary>
        /// Apple Public Source License 1.0
        /// </summary>
        [Description("Apple Public Source License 1.0")] APSL_1_0,

        /// <summary>
        /// Apple Public Source License 1.1
        /// </summary>
        [Description("Apple Public Source License 1.1")] APSL_1_1,

        /// <summary>
        /// Apple Public Source License 1.2
        /// </summary>
        [Description("Apple Public Source License 1.2")] APSL_1_2,

        /// <summary>
        /// Apple Public Source License 2.0
        /// </summary>
        [Description("Apple Public Source License 2.0"), LicenseUri("https://opensource.apple.com/apsl/")] APSL_2_0,

        /// <summary>
        /// Artistic License 1.0
        /// </summary>
        [Description("Artistic License 1.0")] Artistic_1_0,

        /// <summary>
        /// Artistic License 1.0 w/clause 8
        /// </summary>
        [Description("Artistic License 1.0 w/clause 8")] Artistic_1_0_cl8,

        /// <summary>
        /// Artistic License 1.0 (Perl)
        /// </summary>
        [Description("Artistic License 1.0 (Perl)"), LicenseUri("http://dev.perl.org/licenses/artistic.html")] Artistic_1_0_Perl,

        /// <summary>
        /// Artistic License 2.0
        /// </summary>
        [Description("Artistic License 2.0"), LicenseUri("https://www.perlfoundation.org/artistic-license-20.html")] Artistic_2_0,

        /// <summary>
        /// Bahyph License
        /// </summary>
        Bahyph,

        /// <summary>
        /// Barr License
        /// </summary>
        Barr,

        /// <summary>
        /// Beerware License
        /// </summary>
        Beerware,

        /// <summary>
        /// BitTorrent Open Source License v1.0
        /// </summary>
        [Description("BitTorrent Open Source License v1.0")] BitTorrent_1_0,

        /// <summary>
        /// BitTorrent Open Source License v1.1
        /// </summary>
        [Description("BitTorrent Open Source License v1.1")] BitTorrent_1_1,

        /// <summary>
        /// SQLite Blessing
        /// </summary>
        [Description("SQLite Blessing"), LicenseUri("https://sqlite.org/src/raw/LICENSE.md?name=df5091916dbb40e6e9686186587125e1b2ff51f022cc334e886c19a0e9982724")] blessing,

        /// <summary>
        /// Blue Oak Model License 1.0.0
        /// </summary>
        [Description("Blue Oak Model License 1.0.0"), LicenseUri("https://blueoakcouncil.org/license/1.0.0")] BlueOak_1_0_0,

        /// <summary>
        /// Borceux license
        /// </summary>
        Borceux,

        /// <summary>
        /// BSD 1-Clause License
        /// </summary>
        [Description("BSD 1-Clause License"), LicenseUri("https://svnweb.freebsd.org/base/head/include/ifaddrs.h?revision=326823")] BSD_1_Clause,

        /// <summary>
        /// BSD 2-Clause "Simplified" License
        /// </summary>
        [Description("BSD 2-Clause \"Simplified\" License")] BSD_2_Clause,

        /// <summary>
        /// BSD 2-Clause FreeBSD License
        /// </summary>
        [Description("BSD 2-Clause FreeBSD License"), LicenseUri("https://www.freebsd.org/copyright/freebsd-license.html")] BSD_2_Clause_FreeBSD,

        /// <summary>
        /// BSD 2-Clause NetBSD License
        /// </summary>
        [Description("BSD 2-Clause NetBSD License"), LicenseUri("http://www.netbsd.org/about/redistribution.html#default")] BSD_2_Clause_NetBSD,

        /// <summary>
        /// BSD-2-Clause Plus Patent License
        /// </summary>
        [Description("BSD-2-Clause Plus Patent License")] BSD_2_Clause_Patent,

        /// <summary>
        /// BSD 3-Clause "New" or "Revised" License
        /// </summary>
        [Description("BSD 3-Clause \"New\" or \"Revised\" License")] BSD_3_Clause,

        /// <summary>
        /// BSD with attribution
        /// </summary>
        [Description("BSD with attribution")] BSD_3_Clause_Attribution,

        /// <summary>
        /// BSD 3-Clause Clear License
        /// </summary>
        [Description("BSD 3-Clause Clear License")] BSD_3_Clause_Clear,

        /// <summary>
        /// Lawrence Berkeley National Labs BSD variant license
        /// </summary>
        [Description("Lawrence Berkeley National Labs BSD variant license"), Name("BSD-3-Clause-LBNL")] BSD_3_Clause_LBNL,

        /// <summary>
        /// BSD 3-Clause No Nuclear License
        /// </summary>
        [Description("BSD 3-Clause No Nuclear License"), Name("BSD-3-Clause-No-Nuclear-License")] BSD_3_Clause_No_Nuclear_License,

        /// <summary>
        /// BSD 3-Clause No Nuclear License 2014
        /// </summary>
        [Description("BSD 3-Clause No Nuclear License 2014"), Name("BSD-3-Clause-No-Nuclear-License-2014")] BSD_3_Clause_No_Nuclear_License_2014,

        /// <summary>
        /// BSD 3-Clause No Nuclear Warranty
        /// </summary>
        [Description("BSD 3-Clause No Nuclear Warranty"), Name("BSD-3-Clause-No-Nuclear-Warranty")] BSD_3_Clause_No_Nuclear_Warranty,

        /// <summary>
        /// BSD 3-Clause Open MPI variant
        /// </summary>
        [Description("BSD 3-Clause Open MPI variant"), Name("BSD-3-Clause-Open-MPI")] BSD_3_Clause_Open_MPI,

        /// <summary>
        /// BSD 4-Clause "Original" or "Old" License
        /// </summary>
        [Description("BSD 4-Clause \"Original\" or \"Old\" License"), Name("BSD-4-Clause")] BSD_4_Clause,

        /// <summary>
        /// BSD-4-Clause (University of California-Specific)
        /// </summary>
        [Description("BSD-4-Clause (University of California-Specific)"), LicenseUri("https://www.freebsd.org/copyright/license.html"), Name("BSD-4-Clause-UC")] BSD_4_Clause_UC,

        /// <summary>
        /// BSD Protection License
        /// </summary>
        [Description("BSD Protection License")] BSD_Protection,

        /// <summary>
        /// BSD Source Code Attribution
        /// </summary>
        [Description("BSD Source Code Attribution")] BSD_Source_Code,

        /// <summary>
        /// Boost Software License 1.0
        /// </summary>
        [Description("Boost Software License 1.0"), LicenseUri("https://www.boost.org/LICENSE_1_0.txt"), Name("BSL-1.0")] BSL_1_0,

        /// <summary>
        /// bzip2 and libbzip2 License v1.0.5
        /// </summary>
        [Description("bzip2 and libbzip2 License v1.0.5"), Name("bzip2-1.0.5")] bzip2_1_0_5,

        /// <summary>
        /// bzip2 and libbzip2 License v1.0.6
        /// </summary>
        [Description("bzip2 and libbzip2 License v1.0.6"), Name("bzip2-1.0.6")] bzip2_1_0_6,

        /// <summary>
        /// Caldera License
        /// </summary>
        Caldera,

        /// <summary>
        /// Computer Associates Trusted Open Source License 1.1
        /// </summary>
        [Description("Computer Associates Trusted Open Source License 1.1")] CATOSL_1_1,

        /// <summary>
        /// Creative Commons Attribution 1.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution 1.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by/1.0/legalcode")] CC_BY_1_0,

        /// <summary>
        /// Creative Commons Attribution 2.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution 2.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by/2.0/legalcode")] CC_BY_2_0,

        /// <summary>
        /// Creative Commons Attribution 2.5 Generic
        /// </summary>
        [Description("Creative Commons Attribution 2.5 Generic"), LicenseUri("https://creativecommons.org/licenses/by/2.5/legalcode")] CC_BY_2_5,

        /// <summary>
        /// Creative Commons Attribution 3.0 Unported
        /// </summary>
        [Description("Creative Commons Attribution 3.0 Unported"), LicenseUri("https://creativecommons.org/licenses/by/3.0/legalcode")] CC_BY_3_0,

        /// <summary>
        /// Creative Commons Attribution 4.0 International
        /// </summary>
        [Description("Creative Commons Attribution 4.0 International"), LicenseUri("https://creativecommons.org/licenses/by/4.0/legalcode")] CC_BY_4_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial 1.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial 1.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc/1.0/legalcode")] CC_BY_NC_1_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial 2.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial 2.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc/2.0/legalcode")] CC_BY_NC_2_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial 2.5 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial 2.5 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc/2.5/legalcode")] CC_BY_NC_2_5,

        /// <summary>
        /// Creative Commons Attribution Non Commercial 3.0 Unported
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial 3.0 Unported"), LicenseUri("https://creativecommons.org/licenses/by-nc/3.0/legalcode")] CC_BY_NC_3_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial 4.0 International
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial 4.0 International"), LicenseUri("https://creativecommons.org/licenses/by-nc/4.0/legalcode")] CC_BY_NC_4_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial No Derivatives 1.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial No Derivatives 1.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc-nd/1.0/legalcode")] CC_BY_NC_ND_1_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial No Derivatives 2.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial No Derivatives 2.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc-nd/2.0/legalcode")] CC_BY_NC_ND_2_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial No Derivatives 2.5 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial No Derivatives 2.5 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc-nd/2.5/legalcode")] CC_BY_NC_ND_2_5,

        /// <summary>
        /// Creative Commons Attribution Non Commercial No Derivatives 3.0 Unported
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial No Derivatives 3.0 Unported"), LicenseUri("https://creativecommons.org/licenses/by-nc-nd/3.0/legalcode")] CC_BY_NC_ND_3_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial No Derivatives 4.0 International
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial No Derivatives 4.0 International"), LicenseUri("https://creativecommons.org/licenses/by-nc-nd/4.0/legalcode")] CC_BY_NC_ND_4_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial Share Alike 1.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial Share Alike 1.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc-sa/1.0/legalcode")] CC_BY_NC_SA_1_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial Share Alike 2.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial Share Alike 2.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc-sa/2.0/legalcode")] CC_BY_NC_SA_2_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial Share Alike 2.5 Generic
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial Share Alike 2.5 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nc-sa/2.5/legalcode")] CC_BY_NC_SA_2_5,

        /// <summary>
        /// Creative Commons Attribution Non Commercial Share Alike 3.0 Unported
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial Share Alike 3.0 Unported"), LicenseUri("https://creativecommons.org/licenses/by-nc-sa/3.0/legalcode")] CC_BY_NC_SA_3_0,

        /// <summary>
        /// Creative Commons Attribution Non Commercial Share Alike 4.0 International
        /// </summary>
        [Description("Creative Commons Attribution Non Commercial Share Alike 4.0 International"), LicenseUri("https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode")] CC_BY_NC_SA_4_0,

        /// <summary>
        /// Creative Commons Attribution No Derivatives 1.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution No Derivatives 1.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nd/1.0/legalcode")] CC_BY_ND_1_0,

        /// <summary>
        /// Creative Commons Attribution No Derivatives 2.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution No Derivatives 2.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nd/2.0/legalcode")] CC_BY_ND_2_0,

        /// <summary>
        /// Creative Commons Attribution No Derivatives 2.5 Generic
        /// </summary>
        [Description("Creative Commons Attribution No Derivatives 2.5 Generic"), LicenseUri("https://creativecommons.org/licenses/by-nd/2.5/legalcode")] CC_BY_ND_2_5,

        /// <summary>
        /// Creative Commons Attribution No Derivatives 3.0 Unported
        /// </summary>
        [Description("Creative Commons Attribution No Derivatives 3.0 Unported"), LicenseUri("https://creativecommons.org/licenses/by-nd/3.0/legalcode")] CC_BY_ND_3_0,

        /// <summary>
        /// Creative Commons Attribution No Derivatives 4.0 International
        /// </summary>
        [Description("Creative Commons Attribution No Derivatives 4.0 International"), LicenseUri("https://creativecommons.org/licenses/by-nd/4.0/legalcode")] CC_BY_ND_4_0,

        /// <summary>
        /// Creative Commons Attribution Share Alike 1.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Share Alike 1.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-sa/1.0/legalcode")] CC_BY_SA_1_0,

        /// <summary>
        /// Creative Commons Attribution Share Alike 2.0 Generic
        /// </summary>
        [Description("Creative Commons Attribution Share Alike 2.0 Generic"), LicenseUri("https://creativecommons.org/licenses/by-sa/2.0/legalcode")] CC_BY_SA_2_0,

        /// <summary>
        /// Creative Commons Attribution Share Alike 2.5 Generic
        /// </summary>
        [Description("Creative Commons Attribution Share Alike 2.5 Generic"), LicenseUri("https://creativecommons.org/licenses/by-sa/2.5/legalcode")] CC_BY_SA_2_5,

        /// <summary>
        /// Creative Commons Attribution Share Alike 3.0 Unported
        /// </summary>
        [Description("Creative Commons Attribution Share Alike 3.0 Unported"), LicenseUri("https://creativecommons.org/licenses/by-sa/3.0/legalcode")] CC_BY_SA_3_0,

        /// <summary>
        /// Creative Commons Attribution Share Alike 4.0 International
        /// </summary>
        [Description("Creative Commons Attribution Share Alike 4.0 International"), LicenseUri("https://creativecommons.org/licenses/by-sa/4.0/legalcode")] CC_BY_SA_4_0,

        /// <summary>
        /// Creative Commons Public Domain Dedication and Certification
        /// </summary>
        [Description("Creative Commons Public Domain Dedication and Certification"), LicenseUri("https://creativecommons.org/licenses/publicdomain/")] CC_PDDC,

        /// <summary>
        /// Creative Commons Zero v1.0 Universal
        /// </summary>
        [Description("Creative Commons Zero v1.0 Universal"), LicenseUri("https://creativecommons.org/publicdomain/zero/1.0/legalcode")] CC0_1_0,

        /// <summary>
        /// Common Development and Distribution License 1.0
        /// </summary>
        [Description("Common Development and Distribution License 1.0")] CDDL_1_0,

        /// <summary>
        /// Common Development and Distribution License 1.1
        /// </summary>
        [Description("Common Development and Distribution License 1.1")] CDDL_1_1,

        /// <summary>
        /// Community Data License Agreement Permissive 1.0
        /// </summary>
        [Description("Community Data License Agreement Permissive 1.0"), LicenseUri("https://cdla.io/permissive-1-0/")] CDLA_Permissive_1_0,

        /// <summary>
        /// Community Data License Agreement Sharing 1.0
        /// </summary>
        [Description("Community Data License Agreement Sharing 1.0"), LicenseUri("https://cdla.io/sharing-1-0/")] CDLA_Sharing_1_0,

        /// <summary>
        /// CeCILL Free Software License Agreement v1.0
        /// </summary>
        [Description("CeCILL Free Software License Agreement v1.0")] CECILL_1_0,

        /// <summary>
        /// CeCILL Free Software License Agreement v1.1
        /// </summary>
        [Description("CeCILL Free Software License Agreement v1.1")] CECILL_1_1,

        /// <summary>
        /// CeCILL Free Software License Agreement v2.0
        /// </summary>
        [Description("CeCILL Free Software License Agreement v2.0")] CECILL_2_0,

        /// <summary>
        /// CeCILL Free Software License Agreement v2.1
        /// </summary>
        [Description("CeCILL Free Software License Agreement v2.1"), LicenseUri("https://cecill.info/licences/Licence_CeCILL_V2.1-en.html")] CECILL_2_1,

        /// <summary>
        /// CeCILL-B Free Software License Agreement
        /// </summary>
        [Description("CeCILL-B Free Software License Agreement")] CECILL_B,

        /// <summary>
        /// CeCILL-C Free Software License Agreement
        /// </summary>
        [Description("CeCILL-C Free Software License Agreement")] CECILL_C,

        /// <summary>
        /// CERN Open Hardware Licence v1.1
        /// </summary>
        [Description("CERN Open Hardware Licence v1.1")] CERN_OHL_1_1,

        /// <summary>
        /// CERN Open Hardware Licence v1.2
        /// </summary>
        [Description("CERN Open Hardware Licence v1.2")] CERN_OHL_1_2,

        /// <summary>
        /// Clarified Artistic License
        /// </summary>
        [Description("Clarified Artistic License"), LicenseUri("https://www.ncftp.com/ncftp/doc/LICENSE.txt")] ClArtistic,

        /// <summary>
        /// CNRI Jython License
        /// </summary>
        [Description("CNRI Jython License")] CNRI_Jython,

        /// <summary>
        /// CNRI Python License
        /// </summary>
        [Description("CNRI Python License")] CNRI_Python,

        /// <summary>
        /// CNRI Python Open Source GPL Compatible License Agreement
        /// </summary>
        [Description("CNRI Python Open Source GPL Compatible License Agreement")] CNRI_Python_GPL_Compatible,

        /// <summary>
        /// Condor Public License v1.1
        /// </summary>
        [Description("Condor Public License v1.1")] Condor_1_1,

        /// <summary>
        /// copyleft-next 0.3.0
        /// </summary>
        [Description("copyleft-next 0.3.0"), LicenseUri("https://raw.githubusercontent.com/copyleft-next/copyleft-next/master/Releases/copyleft-next-0.3.0")] copyleft_next_0_3_0,

        /// <summary>
        /// copyleft-next 0.3.1
        /// </summary>
        [Description("copyleft-next 0.3.1"), LicenseUri("https://raw.githubusercontent.com/copyleft-next/copyleft-next/master/Releases/copyleft-next-0.3.1")] copyleft_next_0_3_1,

        /// <summary>
        /// Common Public Attribution License 1.0
        /// </summary>
        [Description("Common Public Attribution License 1.0"), LicenseUri("https://spdx.org/licenses/CPAL-1.0.html"), Name("CPAL-1.0")] CPAL_1_0,

        /// <summary>
        /// Common Public License 1.0
        /// </summary>
        [Description("Common Public License 1.0")] CPL_1_0,

        /// <summary>
        /// Code Project Open License 1.02
        /// </summary>
        [Description("Code Project Open License 1.02")] CPOL_1_02,

        /// <summary>
        /// Crossword License
        /// </summary>
        Crossword,

        /// <summary>
        /// CrystalStacker License
        /// </summary>
        CrystalStacker,

        /// <summary>
        /// CUA Office Public License v1.0
        /// </summary>
        [Description("CUA Office Public License v1.0")] CUA_OPL_1_0,

        /// <summary>
        /// Cube License
        /// </summary>
        Cube,

        /// <summary>
        /// curl License
        /// </summary>
        [LicenseUri("https://raw.githubusercontent.com/curl/curl/master/COPYING")] curl,

        /// <summary>
        /// Deutsche Freie Software Lizenz
        /// </summary>
        [Description("Deutsche Freie Software Lizenz"), LicenseUri("https://www.hbz-nrw.de/produkte/open-access/lizenzen/dfsl")] D_FSL_1_0,

        /// <summary>
        /// diffmark license
        /// </summary>
        diffmark,

        /// <summary>
        /// DOC License
        /// </summary>
        DOC,

        /// <summary>
        /// Dotseqn License
        /// </summary>
        Dotseqn,

        /// <summary>
        /// DSDP License
        /// </summary>
        DSDP,

        /// <summary>
        /// dvipdfm License
        /// </summary>
        dvipdfm,

        /// <summary>
        /// Educational Community License v1.0
        /// </summary>
        [Description("Educational Community License v1.0")] ECL_1_0,

        /// <summary>
        /// Educational Community License v2.0
        /// </summary>
        [Description("Educational Community License v2.0")] ECL_2_0,

        /// <summary>
        /// Eiffel Forum License v1.0
        /// </summary>
        [Description("Eiffel Forum License v1.0")] EFL_1_0,

        /// <summary>
        /// Eiffel Forum License v2.0
        /// </summary>
        [Description("Eiffel Forum License v2.0"), LicenseUri("http://www.eiffel-nice.org/license/eiffel-forum-license-2.html")] EFL_2_0,

        /// <summary>
        /// eGenix.com Public License 1.1.0
        /// </summary>
        [Description("eGenix.com Public License 1.1.0"), LicenseUri("http://www.egenix.com/products/eGenix.com-Public-License-1.1.0.pdf")] eGenix,

        /// <summary>
        /// Entessa Public License v1.0
        /// </summary>
        [Description("Entessa Public License v1.0")] Entessa,

        /// <summary>
        /// Eclipse Public License 1.0
        /// </summary>
        [Description("Eclipse Public License 1.0"), LicenseUri("https://www.eclipse.org/legal/epl-v10.html")] EPL_1_0,

        /// <summary>
        /// Eclipse Public License 2.0
        /// </summary>
        [Description("Eclipse Public License 2.0"), LicenseUri("https://www.eclipse.org/legal/epl-2.0/")] EPL_2_0,

        /// <summary>
        /// Erlang Public License v1.1
        /// </summary>
        [Description("Erlang Public License v1.1"), LicenseUri("https://www.erlang.org/EPLICENSE")] ErlPL_1_1,

        /// <summary>
        /// Etalab Open License 2.0
        /// </summary>
        [Description("Etalab Open License 2.0"), LicenseUri("https://raw.githubusercontent.com/DISIC/politique-de-contribution-open-source/master/LICENSE")] etalab_2_0,

        /// <summary>
        /// EU DataGrid Software License
        /// </summary>
        [Description("EU DataGrid Software License")] EUDatagrid,

        /// <summary>
        /// European Union Public License 1.0
        /// </summary>
        [Description("European Union Public License 1.0"), LicenseUri("https://ec.europa.eu/idabc/en/document/7330.html")] EUPL_1_0,

        /// <summary>
        /// European Union Public License 1.1
        /// </summary>
        [Description("European Union Public License 1.1"), LicenseUri("https://joinup.ec.europa.eu/collection/eupl/eupl-text-11-12")] EUPL_1_1,

        /// <summary>
        /// European Union Public License 1.2
        /// </summary>
        [Description("European Union Public License 1.2"), LicenseUri("https://joinup.ec.europa.eu/collection/eupl/eupl-text-11-12")] EUPL_1_2,

        /// <summary>
        /// Eurosym License
        /// </summary>
        Eurosym,

        /// <summary>
        /// Fair License
        /// </summary>
        Fair,

        /// <summary>
        /// Frameworx Open License 1.0
        /// </summary>
        [Description("Frameworx Open License 1.0")] Frameworx_1_0,

        /// <summary>
        /// FreeImage Public License v1.0
        /// </summary>
        [Description("FreeImage Public License v1.0"), LicenseUri("http://freeimage.sourceforge.net/freeimage-license.txt")] FreeImage,

        /// <summary>
        /// FSF All Permissive License
        /// </summary>
        [Description("FSF All Permissive License")] FSFAP,

        /// <summary>
        /// FSF Unlimited License
        /// </summary>
        [Description("FSF Unlimited License")] FSFUL,

        /// <summary>
        /// FSF Unlimited License (with License Retention)
        /// </summary>
        [Description("FSF Unlimited License (with License Retention)")] FSFULLR,

        /// <summary>
        /// Freetype Project License
        /// </summary>
        [Description("Freetype Project License"), LicenseUri("http://git.savannah.gnu.org/cgit/freetype/freetype2.git/plain/docs/FTL.TXT")] FTL,

        /// <summary>
        /// GNU Free Documentation License v1.1 only
        /// </summary>
        [Description("GNU Free Documentation License v1.1 only"), LicenseUri("https://www.gnu.org/licenses/old-licenses/fdl-1.1.txt")] GFDL_1_1_only,

        /// <summary>
        /// GNU Free Documentation License v1.1 or later
        /// </summary>
        [Description("GNU Free Documentation License v1.1 or later"), LicenseUri("https://www.gnu.org/licenses/old-licenses/fdl-1.1.txt")] GFDL_1_1_or_later,

        /// <summary>
        /// GNU Free Documentation License v1.2 only
        /// </summary>
        [Description("GNU Free Documentation License v1.2 only"), LicenseUri("https://www.gnu.org/licenses/old-licenses/fdl-1.2.txt")] GFDL_1_2_only,

        /// <summary>
        /// GNU Free Documentation License v1.2 or later
        /// </summary>
        [Description("GNU Free Documentation License v1.2 or later"), LicenseUri("https://www.gnu.org/licenses/old-licenses/fdl-1.2.txt")] GFDL_1_2_or_later,

        /// <summary>
        /// GNU Free Documentation License v1.3 only
        /// </summary>
        [Description("GNU Free Documentation License v1.3 only"), LicenseUri("https://www.gnu.org/licenses/fdl-1.3.txt")] GFDL_1_3_only,

        /// <summary>
        /// GNU Free Documentation License v1.3 or later
        /// </summary>
        [Description("GNU Free Documentation License v1.3 or later"), LicenseUri("https://www.gnu.org/licenses/fdl-1.3.txt")] GFDL_1_3_or_later,

        /// <summary>
        /// Giftware License
        /// </summary>
        [LicenseUri("https://liballeg.org/license.html")] Giftware,

        /// <summary>
        /// GL2PS License
        /// </summary>
        [LicenseUri("http://www.geuz.org/gl2ps/COPYING.GL2PS")] GL2PS,

        /// <summary>
        /// 3dfx Glide License
        /// </summary>
        [Description("3dfx Glide License"), LicenseUri("http://wenchy.net/old/glidexp/COPYING.txt")] Glide,

        /// <summary>
        /// Glulxe License
        /// </summary>
        Glulxe,

        /// <summary>
        /// gnuplot License
        /// </summary>
        gnuplot,

        /// <summary>
        /// GNU General Public License v1.0 only
        /// </summary>
        [Description("GNU General Public License v1.0 only"), LicenseUri("https://www.gnu.org/licenses/old-licenses/gpl-1.0.txt")] GPL_1_0_only,

        /// <summary>
        /// GNU General Public License v1.0 or later
        /// </summary>
        [Description("GNU General Public License v1.0 or later"), LicenseUri("https://www.gnu.org/licenses/old-licenses/gpl-1.0.txt")] GPL_1_0_or_later,

        /// <summary>
        /// GNU General Public License v2.0 only
        /// </summary>
        [Description("GNU General Public License v2.0 only"), LicenseUri("https://www.gnu.org/licenses/old-licenses/gpl-2.0.txt")] GPL_2_0_only,

        /// <summary>
        /// GNU General Public License v2.0 or later
        /// </summary>
        [Description("GNU General Public License v2.0 or later"), LicenseUri("https://www.gnu.org/licenses/old-licenses/gpl-2.0.txt")] GPL_2_0_or_later,

        /// <summary>
        /// GNU General Public License v3.0 only
        /// </summary>
        [Description("GNU General Public License v3.0 only"), LicenseUri("https://www.gnu.org/licenses/gpl-3.0.txt")] GPL_3_0_only,

        /// <summary>
        /// GNU General Public License v3.0 or later
        /// </summary>
        [Description("GNU General Public License v3.0 or later"), LicenseUri("https://www.gnu.org/licenses/gpl-3.0.txt")] GPL_3_0_or_later,

        /// <summary>
        /// gSOAP Public License v1.3b
        /// </summary>
        [Description("gSOAP Public License v1.3b"), LicenseUri("http://www.cs.fsu.edu/~engelen/license.html"), Name("gSOAP-1.3b")] gSOAP_1_3b,

        /// <summary>
        /// Haskell Language Report License
        /// </summary>
        [Description("Haskell Language Report License")] HaskellReport,

        /// <summary>
        /// Historical Permission Notice and Disclaimer
        /// </summary>
        [Description("Historical Permission Notice and Disclaimer")] HPND,

        /// <summary>
        /// Historical Permission Notice and Disclaimer - sell variant
        /// </summary>
        [Description("Historical Permission Notice and Disclaimer - sell variant")] HPND_sell_variant,

        /// <summary>
        /// IBM PowerPC Initialization and Boot Software
        /// </summary>
        [Description("IBM PowerPC Initialization and Boot Software")] IBM_pibs,

        /// <summary>
        /// ICU License
        /// </summary>
        ICU,

        /// <summary>
        /// Independent JPEG Group License
        /// </summary>
        [Description("Independent JPEG Group License"), LicenseUri("https://dev.w3.org/cvsweb/Amaya/libjpeg/Attic/README?rev=1.2;content-type=text%2Fplain")] IJG,

        /// <summary>
        /// ImageMagick License
        /// </summary>
        [Description("ImageMagick License"), LicenseUri("http://www.imagemagick.org/script/license.php")] ImageMagick,

        /// <summary>
        /// iMatix Standard Function Library Agreement
        /// </summary>
        [Description("iMatix Standard Function Library Agreement")] iMatix,

        /// <summary>
        /// Imlib2 License
        /// </summary>
        [LicenseUri("https://git.enlightenment.org/legacy/imlib2.git/plain/COPYING")] Imlib2,

        /// <summary>
        /// Info-ZIP License
        /// </summary>
        [LicenseUri("http://infozip.sourceforge.net/license.html")] Info_ZIP,

        /// <summary>
        /// Intel Open Source License
        /// </summary>
        [Description("Intel Open Source License")] Intel,

        /// <summary>
        /// Intel ACPI Software License Agreement
        /// </summary>
        [Description("Intel ACPI Software License Agreement")] Intel_ACPI,

        /// <summary>
        /// Interbase Public License v1.0
        /// </summary>
        [Description("Interbase Public License v1.0")] Interbase_1_0,

        /// <summary>
        /// IPA Font License
        /// </summary>
        [Description("IPA Font License")] IPA,

        /// <summary>
        /// IBM Public License v1.0
        /// </summary>
        [Description("IBM Public License v1.0")] IPL_1_0,

        /// <summary>
        /// ISC License
        /// </summary>
        [LicenseUri("https://gitlab.isc.org/isc-projects/bind9/raw/master/COPYRIGHT")] ISC,

        /// <summary>
        /// JasPer License
        /// </summary>
        [Description("JasPer License"), LicenseUri("https://www.ece.uvic.ca/~frodo/jasper/LICENSE")] JasPer_2_0,

        /// <summary>
        /// Japan Network Information Center License
        /// </summary>
        [Description("Japan Network Information Center License")] JPNIC,

        /// <summary>
        /// JSON License
        /// </summary>
        [LicenseUri("http://www.json.org/license.html")] JSON,

        /// <summary>
        /// Licence Art Libre 1.2
        /// </summary>
        [Description("Licence Art Libre 1.2"), LicenseUri("http://artlibre.org/licence/lal/licence-art-libre-12/")] LAL_1_2,

        /// <summary>
        /// Licence Art Libre 1.3
        /// </summary>
        [Description("Licence Art Libre 1.3"), LicenseUri("https://artlibre.org/")] LAL_1_3,

        /// <summary>
        /// Latex2e License
        /// </summary>
        Latex2e,

        /// <summary>
        /// Leptonica License
        /// </summary>
        Leptonica,

        /// <summary>
        /// GNU Library General Public License v2 only
        /// </summary>
        [Description("GNU Library General Public License v2 only"), LicenseUri("https://www.gnu.org/licenses/old-licenses/lgpl-2.0.txt")] LGPL_2_0_only,

        /// <summary>
        /// GNU Library General Public License v2 or later
        /// </summary>
        [Description("GNU Library General Public License v2 or later"), LicenseUri("https://www.gnu.org/licenses/old-licenses/lgpl-2.0.txt")] LGPL_2_0_or_later,

        /// <summary>
        /// GNU Lesser General Public License v2.1 only
        /// </summary>
        [Description("GNU Lesser General Public License v2.1 only"), LicenseUri("https://www.gnu.org/licenses/old-licenses/lgpl-2.1.txt")] LGPL_2_1_only,

        /// <summary>
        /// GNU Lesser General Public License v2.1 or later
        /// </summary>
        [Description("GNU Lesser General Public License v2.1 or later"), LicenseUri("https://www.gnu.org/licenses/old-licenses/lgpl-2.0.txt")] LGPL_2_1_or_later,

        /// <summary>
        /// GNU Lesser General Public License v3.0 only
        /// </summary>
        [Description("GNU Lesser General Public License v3.0 only"), LicenseUri("https://www.gnu.org/licenses/lgpl-3.0.txt")] LGPL_3_0_only,

        /// <summary>
        /// GNU Lesser General Public License v3.0 or later
        /// </summary>
        [Description("GNU Lesser General Public License v3.0 or later"), LicenseUri("https://www.gnu.org/licenses/lgpl-3.0.txt")] LGPL_3_0_or_later,

        /// <summary>
        /// Lesser General Public License For Linguistic Resources
        /// </summary>
        [Description("Lesser General Public License For Linguistic Resources"), LicenseUri("https://raw.githubusercontent.com/UnitexGramLab/LGPLLR/master/LGPLLR")] LGPLLR,

        /// <summary>
        /// libpng License
        /// </summary>
        [LicenseUri("http://www.libpng.org/pub/png/src/libpng-LICENSE.txt")] Libpng,

        /// <summary>
        /// PNG Reference Library version 2
        /// </summary>
        [Description("PNG Reference Library version 2"), LicenseUri("http://www.libpng.org/pub/png/src/libpng-LICENSE.txt")] libpng_2_0,

        /// <summary>
        /// libtiff License
        /// </summary>
        libtiff,

        /// <summary>
        /// Licence Libre du Québec – Permissive version 1.1
        /// </summary>
        [Description("Licence Libre du Québec – Permissive version 1.1"), LicenseUri("https://forge.gouv.qc.ca/licence/liliq-v1-1/")] LiLiQ_P_1_1,

        /// <summary>
        /// Licence Libre du Québec – Réciprocité version 1.1
        /// </summary>
        [Description("Licence Libre du Québec – Réciprocité version 1.1"), LicenseUri("https://forge.gouv.qc.ca/licence/liliq-v1-1/#réciprocité-liliq-r")] LiLiQ_R_1_1,

        /// <summary>
        /// Licence Libre du Québec – Réciprocité forte version 1.1
        /// </summary>
        [Description("Licence Libre du Québec – Réciprocité forte version 1.1"), LicenseUri("https://forge.gouv.qc.ca/licence/liliq-v1-1/#réciprocité-forte-liliq-r")] LiLiQ_Rplus_1_1,

        /// <summary>
        /// Linux Kernel Variant of OpenIB.org license
        /// </summary>
        [Description("Linux Kernel Variant of OpenIB.org license")] Linux_OpenIB,

        /// <summary>
        /// Lucent Public License Version 1.0
        /// </summary>
        [Description("Lucent Public License Version 1.0")] LPL_1_0,

        /// <summary>
        /// Lucent Public License v1.02
        /// </summary>
        [Description("Lucent Public License v1.02")] LPL_1_02,

        /// <summary>
        /// LaTeX Project Public License v1.0
        /// </summary>
        [Description("LaTeX Project Public License v1.0"), LicenseUri("https://www.latex-project.org/lppl/lppl-1-0.txt")] LPPL_1_0,

        /// <summary>
        /// LaTeX Project Public License v1.1
        /// </summary>
        [Description("LaTeX Project Public License v1.1"), LicenseUri("https://www.latex-project.org/lppl/lppl-1-1.txt")] LPPL_1_1,

        /// <summary>
        /// LaTeX Project Public License v1.2
        /// </summary>
        [Description("LaTeX Project Public License v1.2"), LicenseUri("https://www.latex-project.org/lppl/lppl-1-2.txt")] LPPL_1_2,

        /// <summary>
        /// LaTeX Project Public License v1.3a
        /// </summary>
        [Description("LaTeX Project Public License v1.3a"), LicenseUri("https://www.latex-project.org/lppl/lppl-1-3a.txt"), Name("LPPL-1.3a")] LPPL_1_3a,

        /// <summary>
        /// LaTeX Project Public License v1.3c
        /// </summary>
        [Description("LaTeX Project Public License v1.3c"), LicenseUri("https://www.latex-project.org/lppl/lppl-1-3c.txt"), Name("LPPL-1.3c")] LPPL_1_3c,

        /// <summary>
        /// MakeIndex License
        /// </summary>
        MakeIndex,

        /// <summary>
        /// The MirOS Licence
        /// </summary>
        MirOS,

        /// <summary>
        /// MIT License
        /// </summary>
        MIT,

        /// <summary>
        /// MIT No Attribution
        /// </summary>
        [Description("MIT No Attribution")] MIT_0,

        /// <summary>
        /// Enlightenment License (e16)
        /// </summary>
        [Description("Enlightenment License (e16)")] MIT_advertising,

        /// <summary>
        /// CMU License
        /// </summary>
        [Description("CMU License")] MIT_CMU,

        /// <summary>
        /// enna License
        /// </summary>
        [Description("enna License")] MIT_enna,

        /// <summary>
        /// feh License
        /// </summary>
        [Description("feh License")] MIT_feh,

        /// <summary>
        /// MIT +no-false-attribs license
        /// </summary>
        [Description("MIT +no-false-attribs license")] MITNFA,

        /// <summary>
        /// Motosoto License
        /// </summary>
        Motosoto,

        /// <summary>
        /// mpich2 License
        /// </summary>
        mpich2,

        /// <summary>
        /// Mozilla Public License 1.0
        /// </summary>
        [Description("Mozilla Public License 1.0"), LicenseUri("https://website-archive.mozilla.org/www.mozilla.org/mpl/mpl/1.0/")] MPL_1_0,

        /// <summary>
        /// Mozilla Public License 1.1
        /// </summary>
        [Description("Mozilla Public License 1.1"), LicenseUri("https://www.mozilla.org/media/MPL/1.1/index.0c5913925d40.txt"), Name("MPL-1.1")] MPL_1_1,

        /// <summary>
        /// Mozilla Public License 2.0
        /// </summary>
        [Description("Mozilla Public License 2.0"), LicenseUri("https://www.mozilla.org/en-US/MPL/2.0/"), Name("MPL-2.0")] MPL_2_0,

        /// <summary>
        /// Mozilla Public License 2.0 (no copyleft exception)
        /// </summary>
        [Description("Mozilla Public License 2.0 (no copyleft exception)"), LicenseUri("https://www.mozilla.org/en-US/MPL/2.0/")] MPL_2_0_no_copyleft_exception,

        /// <summary>
        /// Microsoft Public License
        /// </summary>
        [Description("Microsoft Public License")] MS_PL,

        /// <summary>
        /// Microsoft Reciprocal License
        /// </summary>
        [Description("Microsoft Reciprocal License")] MS_RL,

        /// <summary>
        /// Matrix Template Library License
        /// </summary>
        [Description("Matrix Template Library License")] MTLL,

        /// <summary>
        /// Mulan Permissive Software License, Version 1
        /// </summary>
        [Description("Mulan Permissive Software License, Version 1"), LicenseUri("https://license.coscl.org.cn/MulanPSL/")] MulanPSL_1_0,

        /// <summary>
        /// Multics License
        /// </summary>
        Multics,

        /// <summary>
        /// Mup License
        /// </summary>
        Mup,

        /// <summary>
        /// NASA Open Source Agreement 1.3
        /// </summary>
        [Description("NASA Open Source Agreement 1.3"), LicenseUri("https://ti.arc.nasa.gov/opensource/nosa/")] NASA_1_3,

        /// <summary>
        /// Naumen Public License
        /// </summary>
        [Description("Naumen Public License")] Naumen,

        /// <summary>
        /// Net Boolean Public License v1
        /// </summary>
        [Description("Net Boolean Public License v1"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=37b4b3f6cc4bf34e1d3dec61e69914b9819d8894")] NBPL_1_0,

        /// <summary>
        /// University of Illinois/NCSA Open Source License
        /// </summary>
        [Description("University of Illinois/NCSA Open Source License")] NCSA,

        /// <summary>
        /// Net-SNMP License
        /// </summary>
        [LicenseUri("http://net-snmp.sourceforge.net/about/license.html")] Net_SNMP,

        /// <summary>
        /// NetCDF license
        /// </summary>
        [LicenseUri("https://www.unidata.ucar.edu/software/netcdf/copyright.html")] NetCDF,

        /// <summary>
        /// Newsletr License
        /// </summary>
        Newsletr,

        /// <summary>
        /// Nethack General Public License
        /// </summary>
        [Description("Nethack General Public License")] NGPL,

        /// <summary>
        /// Norwegian Licence for Open Government Data
        /// </summary>
        [Description("Norwegian Licence for Open Government Data"), LicenseUri("http://data.norge.no/nlod/en/1.0")] NLOD_1_0,

        /// <summary>
        /// No Limit Public License
        /// </summary>
        [Description("No Limit Public License")] NLPL,

        /// <summary>
        /// Nokia Open Source License
        /// </summary>
        [Description("Nokia Open Source License")] Nokia,

        /// <summary>
        /// Netizen Open Source License
        /// </summary>
        [Description("Netizen Open Source License")] NOSL,

        /// <summary>
        /// Noweb License
        /// </summary>
        Noweb,

        /// <summary>
        /// Netscape Public License v1.0
        /// </summary>
        [Description("Netscape Public License v1.0"), LicenseUri("https://website-archive.mozilla.org/www.mozilla.org/mpl/mpl/npl/1.0/")] NPL_1_0,

        /// <summary>
        /// Netscape Public License v1.1
        /// </summary>
        [Description("Netscape Public License v1.1"), LicenseUri("https://website-archive.mozilla.org/www.mozilla.org/mpl/mpl/npl/1.1/")] NPL_1_1,

        /// <summary>
        /// Non-Profit Open Software License 3.0
        /// </summary>
        [Description("Non-Profit Open Software License 3.0")] NPOSL_3_0,

        /// <summary>
        /// NRL License
        /// </summary>
        [LicenseUri("http://web.mit.edu/network/isakmp/nrllicense.html")] NRL,

        /// <summary>
        /// NTP License
        /// </summary>
        NTP,

        /// <summary>
        /// Open CASCADE Technology Public License
        /// </summary>
        [Description("Open CASCADE Technology Public License"), LicenseUri("https://www.opencascade.com/content/occt-public-license")] OCCT_PL,

        /// <summary>
        /// OCLC Research Public License 2.0
        /// </summary>
        [Description("OCLC Research Public License 2.0")] OCLC_2_0,

        /// <summary>
        /// ODC Open Database License v1.0
        /// </summary>
        [Description("ODC Open Database License v1.0"), LicenseUri("https://www.opendatacommons.org/licenses/odbl/1.0/")] ODbL_1_0,

        /// <summary>
        /// Open Data Commons Attribution License v1.0
        /// </summary>
        [Description("Open Data Commons Attribution License v1.0"), LicenseUri("https://opendatacommons.org/files/2018/02/odc_by_1.0_public_text.txt")] ODC_By_1_0,

        /// <summary>
        /// SIL Open Font License 1.0
        /// </summary>
        [Description("SIL Open Font License 1.0"), LicenseUri("https://scripts.sil.org/OFL10_web")] OFL_1_0,

        /// <summary>
        /// SIL Open Font License 1.1
        /// </summary>
        [Description("SIL Open Font License 1.1"), LicenseUri("https://scripts.sil.org/OFL_web")] OFL_1_1,

        /// <summary>
        /// Open Government Licence - Canada
        /// </summary>
        [Description("Open Government Licence - Canada"), LicenseUri("https://open.canada.ca/en/open-government-licence-canada")] OGL_Canada_2_0,

        /// <summary>
        /// Open Government Licence v1.0
        /// </summary>
        [Description("Open Government Licence v1.0"), LicenseUri("http://www.nationalarchives.gov.uk/doc/open-government-licence/version/1/")] OGL_UK_1_0,

        /// <summary>
        /// Open Government Licence v2.0
        /// </summary>
        [Description("Open Government Licence v2.0"), LicenseUri("http://www.nationalarchives.gov.uk/doc/open-government-licence/version/2/")] OGL_UK_2_0,

        /// <summary>
        /// Open Government Licence v3.0
        /// </summary>
        [Description("Open Government Licence v3.0"), LicenseUri("http://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/")] OGL_UK_3_0,

        /// <summary>
        /// Open Group Test Suite License
        /// </summary>
        [Description("Open Group Test Suite License"), LicenseUri("http://www.opengroup.org/testing/downloads/The_Open_Group_TSL.txt")] OGTSL,

        /// <summary>
        /// Open LDAP Public License v1.1
        /// </summary>
        [Description("Open LDAP Public License v1.1"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=806557a5ad59804ef3a44d5abfbe91d706b0791f")] OLDAP_1_1,

        /// <summary>
        /// Open LDAP Public License v1.2
        /// </summary>
        [Description("Open LDAP Public License v1.2"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=42b0383c50c299977b5893ee695cf4e486fb0dc7")] OLDAP_1_2,

        /// <summary>
        /// Open LDAP Public License v1.3
        /// </summary>
        [Description("Open LDAP Public License v1.3"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=e5f8117f0ce088d0bd7a8e18ddf37eaa40eb09b1")] OLDAP_1_3,

        /// <summary>
        /// Open LDAP Public License v1.4
        /// </summary>
        [Description("Open LDAP Public License v1.4"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=c9f95c2f3f2ffb5e0ae55fe7388af75547660941")] OLDAP_1_4,

        /// <summary>
        /// Open LDAP Public License v2.0 (or possibly 2.0A and 2.0B)
        /// </summary>
        [Description("Open LDAP Public License v2.0"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=cbf50f4e1185a21abd4c0a54d3f4341fe28f36ea")] OLDAP_2_0,

        /// <summary>
        /// Open LDAP Public License v2.0.1
        /// </summary>
        [Description("Open LDAP Public License v2.0.1"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=b6d68acd14e51ca3aab4428bf26522aa74873f0e")] OLDAP_2_0_1,

        /// <summary>
        /// Open LDAP Public License v2.1
        /// </summary>
        [Description("Open LDAP Public License v2.1"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=b0d176738e96a0d3b9f85cb51e140a86f21be715")] OLDAP_2_1,

        /// <summary>
        /// Open LDAP Public License v2.2
        /// </summary>
        [Description("Open LDAP Public License v2.2"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=470b0c18ec67621c85881b2733057fecf4a1acc3")] OLDAP_2_2,

        /// <summary>
        /// Open LDAP Public License v2.2.1
        /// </summary>
        [Description("Open LDAP Public License v2.2.1"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=4bc786f34b50aa301be6f5600f58a980070f481e")] OLDAP_2_2_1,

        /// <summary>
        /// Open LDAP Public License 2.2.2
        /// </summary>
        [Description("Open LDAP Public License 2.2.2"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=df2cc1e21eb7c160695f5b7cffd6296c151ba188")] OLDAP_2_2_2,

        /// <summary>
        /// Open LDAP Public License v2.3
        /// </summary>
        [Description("Open LDAP Public License v2.3"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=d32cf54a32d581ab475d23c810b0a7fbaf8d63c3")] OLDAP_2_3,

        /// <summary>
        /// Open LDAP Public License v2.4
        /// </summary>
        [Description("Open LDAP Public License v2.4"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=cd1284c4a91a8a380d904eee68d1583f989ed386")] OLDAP_2_4,

        /// <summary>
        /// Open LDAP Public License v2.5
        /// </summary>
        [Description("Open LDAP Public License v2.5"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=6852b9d90022e8593c98205413380536b1b5a7cf")] OLDAP_2_5,

        /// <summary>
        /// Open LDAP Public License v2.6
        /// </summary>
        [Description("Open LDAP Public License v2.6"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=1cae062821881f41b73012ba816434897abf4205")] OLDAP_2_6,

        /// <summary>
        /// Open LDAP Public License v2.7
        /// </summary>
        [Description("Open LDAP Public License v2.7"), LicenseUri("http://www.openldap.org/devel/gitweb.cgi?p=openldap.git;a=blob_plain;f=LICENSE;hb=47c2415c1df81556eeb39be6cad458ef87c534a2")] OLDAP_2_7,

        /// <summary>
        /// Open LDAP Public License v2.8
        /// </summary>
        [Description("Open LDAP Public License v2.8"), LicenseUri("http://www.openldap.org/software/release/license.html")] OLDAP_2_8,

        /// <summary>
        /// Open Market License
        /// </summary>
        [Description("Open Market License")] OML,

        /// <summary>
        /// OpenSSL License
        /// </summary>
        [Description("OpenSSL License"), LicenseUri("https://www.openssl.org/source/license.html")] OpenSSL,

        /// <summary>
        /// Open Public License v1.0
        /// </summary>
        [Description("Open Public License v1.0")] OPL_1_0,

        /// <summary>
        /// OSET Public License version 2.1
        /// </summary>
        [Description("OSET Public License version 2.1"), LicenseUri("https://www.osetfoundation.org/public-license")] OSET_PL_2_1,

        /// <summary>
        /// Open Software License 1.0
        /// </summary>
        [Description("Open Software License 1.0")] OSL_1_0,

        /// <summary>
        /// Open Software License 1.1
        /// </summary>
        [Description("Open Software License 1.1")] OSL_1_1,

        /// <summary>
        /// Open Software License 2.0
        /// </summary>
        [Description("Open Software License 2.0")] OSL_2_0,

        /// <summary>
        /// Open Software License 2.1
        /// </summary>
        [Description("Open Software License 2.1")] OSL_2_1,

        /// <summary>
        /// Open Software License 3.0
        /// </summary>
        [Description("Open Software License 3.0"), LicenseUri("https://web.archive.org/web/20120101081418/http://rosenlaw.com:80/OSL3.0.htm")] OSL_3_0,

        /// <summary>
        /// The Parity Public License 6.0.0
        /// </summary>
        [Description("The Parity Public License 6.0.0"), LicenseUri("https://paritylicense.com/versions/6.0.0.html")] Parity_6_0_0,

        /// <summary>
        /// ODC Public Domain Dedication &amp; License 1.0
        /// </summary>
        [Description("ODC Public Domain Dedication & License 1.0"), LicenseUri("https://opendatacommons.org/licenses/pddl/1.0/")] PDDL_1_0,

        /// <summary>
        /// PHP License v3.0
        /// </summary>
        [Description("PHP License v3.0"), LicenseUri("https://www.php.net/license/3_0.txt")] PHP_3_0,

        /// <summary>
        /// PHP License v3.01
        /// </summary>
        [Description("PHP License v3.01"), LicenseUri("https://www.php.net/license/3_01.txt")] PHP_3_01,

        /// <summary>
        /// Plexus Classworlds License
        /// </summary>
        [Description("Plexus Classworlds License")] Plexus,

        /// <summary>
        /// PostgreSQL License
        /// </summary>
        [Description("PostgreSQL License"), LicenseUri("https://www.postgresql.org/about/licence/")] PostgreSQL,

        /// <summary>
        /// psfrag License
        /// </summary>
        psfrag,

        /// <summary>
        /// psutils License
        /// </summary>
        psutils,

        /// <summary>
        /// Python License 2.0
        /// </summary>
        [Description("Python License 2.0")] Python_2_0,

        /// <summary>
        /// Qhull License
        /// </summary>
        Qhull,

        /// <summary>
        /// Q Public License 1.0
        /// </summary>
        [Description("Q Public License 1.0")] QPL_1_0,

        /// <summary>
        /// Rdisc License
        /// </summary>
        Rdisc,

        /// <summary>
        /// Red Hat eCos Public License v1.1
        /// </summary>
        [Description("Red Hat eCos Public License v1.1"), LicenseUri("http://ecos.sourceware.org/old-license.html")] RHeCos_1_1,

        /// <summary>
        /// Reciprocal Public License 1.1
        /// </summary>
        [Description("Reciprocal Public License 1.1")] RPL_1_1,

        /// <summary>
        /// Reciprocal Public License 1.5
        /// </summary>
        [Description("Reciprocal Public License 1.5")] RPL_1_5,

        /// <summary>
        /// RealNetworks Public Source License v1.0
        /// </summary>
        [Description("RealNetworks Public Source License v1.0")] RPSL_1_0,

        /// <summary>
        /// RSA Message-Digest License 
        /// </summary>
        [Description("RSA Message-Digest License ")] RSA_MD,

        /// <summary>
        /// Ricoh Source Code Public License
        /// </summary>
        [Description("Ricoh Source Code Public License")] RSCPL,

        /// <summary>
        /// Ruby License
        /// </summary>
        [LicenseUri("http://www.ruby-lang.org/en/about/license.txt")] Ruby,

        /// <summary>
        /// Sax Public Domain Notice
        /// </summary>
        [Description("Sax Public Domain Notice"), LicenseUri("http://www.saxproject.org/copying.html")] SAX_PD,

        /// <summary>
        /// Saxpath License
        /// </summary>
        Saxpath,

        /// <summary>
        /// SCEA Shared Source License
        /// </summary>
        [Description("SCEA Shared Source License")] SCEA,

        /// <summary>
        /// Sendmail License
        /// </summary>
        [LicenseUri("http://www.sendmail.com/pdfs/open_source/sendmail_license.pdf")] Sendmail,

        /// <summary>
        /// Sendmail License 8.23
        /// </summary>
        [Description("Sendmail License 8.23"), LicenseUri("https://www.proofpoint.com/sites/default/files/sendmail-license.pdf")] Sendmail_8_23,

        /// <summary>
        /// SGI Free Software License B v1.0
        /// </summary>
        [Description("SGI Free Software License B v1.0")] SGI_B_1_0,

        /// <summary>
        /// SGI Free Software License B v1.1
        /// </summary>
        [Description("SGI Free Software License B v1.1")] SGI_B_1_1,

        /// <summary>
        /// SGI Free Software License B v2.0
        /// </summary>
        [Description("SGI Free Software License B v2.0")] SGI_B_2_0,

        /// <summary>
        /// Solderpad Hardware License v0.5
        /// </summary>
        [Description("Solderpad Hardware License v0.5"), LicenseUri("https://solderpad.org/licenses/SHL-0.5/")] SHL_0_5,

        /// <summary>
        /// Solderpad Hardware License, Version 0.51
        /// </summary>
        [Description("Solderpad Hardware License, Version 0.51"), LicenseUri("https://solderpad.org/licenses/SHL-0.51/")] SHL_0_51,

        /// <summary>
        /// Simple Public License 2.0
        /// </summary>
        [Description("Simple Public License 2.0")] SimPL_2_0,

        /// <summary>
        /// Sun Industry Standards Source License v1.1
        /// </summary>
        [Description("Sun Industry Standards Source License v1.1"), LicenseUri("http://www.openoffice.org/licenses/sissl_license.html")] SISSL,

        /// <summary>
        /// Sun Industry Standards Source License v1.2
        /// </summary>
        [Description("Sun Industry Standards Source License v1.2"), LicenseUri("http://gridscheduler.sourceforge.net/Gridengine_SISSL_license.html")] SISSL_1_2,

        /// <summary>
        /// Sleepycat License
        /// </summary>
        Sleepycat,

        /// <summary>
        /// Standard ML of New Jersey License
        /// </summary>
        [Description("Standard ML of New Jersey License"), LicenseUri("https://www.smlnj.org/license.html")] SMLNJ,

        /// <summary>
        /// Secure Messaging Protocol Public License
        /// </summary>
        [Description("Secure Messaging Protocol Public License"), LicenseUri("https://raw.githubusercontent.com/dcblake/SMP/master/Documentation/License.txt")] SMPPL,

        /// <summary>
        /// SNIA Public License 1.1
        /// </summary>
        [Description("SNIA Public License 1.1")] SNIA,

        /// <summary>
        /// Spencer License 86
        /// </summary>
        [Description("Spencer License 86"), Name("Spencer-86")] Spencer_86,

        /// <summary>
        /// Spencer License 94
        /// </summary>
        [Description("Spencer License 94"), Name("Spencer-94")] Spencer_94,

        /// <summary>
        /// Spencer License 99
        /// </summary>
        [Description("Spencer License 99"), Name("Spencer-99")] Spencer_99,

        /// <summary>
        /// Sun Public License v1.0
        /// </summary>
        [Description("Sun Public License v1.0")] SPL_1_0,

        /// <summary>
        /// SSH OpenSSH license
        /// </summary>
        [Description("SSH OpenSSH license"), LicenseUri("https://raw.githubusercontent.com/openssh/openssh-portable/1b11ea7c58cd5c59838b5fa574cd456d6047b2d4/LICENCE")] SSH_OpenSSH,

        /// <summary>
        /// SSH short notice
        /// </summary>
        [Description("SSH short notice")] SSH_short,

        /// <summary>
        /// Server Side Public License, v 1
        /// </summary>
        [Description("Server Side Public License, v 1"), LicenseUri("https://www.mongodb.com/licensing/server-side-public-license")] SSPL_1_0,

        /// <summary>
        /// SugarCRM Public License v1.1.3
        /// </summary>
        [Description("SugarCRM Public License v1.1.3")] SugarCRM_1_1_3,

        /// <summary>
        /// Scheme Widget Library (SWL) Software License Agreement
        /// </summary>
        [Description("Scheme Widget Library (SWL) Software License Agreement")] SWL,

        /// <summary>
        /// TAPR Open Hardware License v1.0
        /// </summary>
        [Description("TAPR Open Hardware License v1.0"), LicenseUri("https://www.tapr.org/TAPR_Open_Hardware_License_v1.0.txt")] TAPR_OHL_1_0,

        /// <summary>
        /// TCL/TK License
        /// </summary>
        [Description("TCL/TK License"), LicenseUri("http://www.tcl.tk/software/tcltk/license.html")] TCL,

        /// <summary>
        /// TCP Wrappers License
        /// </summary>
        [Description("TCP Wrappers License")] TCP_wrappers,

        /// <summary>
        /// TMate Open Source License
        /// </summary>
        [Description("TMate Open Source License"), LicenseUri("https://svnkit.com/license.html")] TMate,

        /// <summary>
        /// TORQUE v2.5+ Software License v1.1
        /// </summary>
        [Description("TORQUE v2.5+ Software License v1.1")] TORQUE_1_1,

        /// <summary>
        /// Trusster Open Source License
        /// </summary>
        [Description("Trusster Open Source License")] TOSL,

        /// <summary>
        /// Technische Universitaet Berlin License 1.0
        /// </summary>
        [Description("Technische Universitaet Berlin License 1.0"), LicenseUri("https://raw.githubusercontent.com/swh/ladspa/7bf6f3799fdba70fda297c2d8fd9f526803d9680/gsm/COPYRIGHT")] TU_Berlin_1_0,

        /// <summary>
        /// Technische Universitaet Berlin License 2.0
        /// </summary>
        [Description("Technische Universitaet Berlin License 2.0"), LicenseUri("https://raw.githubusercontent.com/CorsixTH/deps/fd339a9f526d1d9c9f01ccf39e438a015da50035/licences/libgsm.txt")] TU_Berlin_2_0,

        /// <summary>
        /// Upstream Compatibility License v1.0
        /// </summary>
        [Description("Upstream Compatibility License v1.0")] UCL_1_0,

        /// <summary>
        /// Unicode License Agreement - Data Files and Software (2015)
        /// </summary>
        [Description("Unicode License Agreement - Data Files and Software (2015)"), LicenseUri("http://www.unicode.org/copyright.html")] Unicode_DFS_2015,

        /// <summary>
        /// Unicode License Agreement - Data Files and Software (2016)
        /// </summary>
        [Description("Unicode License Agreement - Data Files and Software (2016)"), LicenseUri("http://www.unicode.org/copyright.html")] Unicode_DFS_2016,

        /// <summary>
        /// Unicode Terms of Use
        /// </summary>
        [Description("Unicode Terms of Use"), LicenseUri("http://www.unicode.org/copyright.html")] Unicode_TOU,

        /// <summary>
        /// The Unlicense
        /// </summary>
        [Description("The Unlicense"), LicenseUri("https://unlicense.org/")] Unlicense,

        /// <summary>
        /// Universal Permissive License v1.0
        /// </summary>
        [Description("Universal Permissive License v1.0")] UPL_1_0,

        /// <summary>
        /// Vim License
        /// </summary>
        [LicenseUri("http://vimdoc.sourceforge.net/htmldoc/uganda.html")] Vim,

        /// <summary>
        /// VOSTROM Public License for Open Source
        /// </summary>
        [Description("VOSTROM Public License for Open Source")] VOSTROM,

        /// <summary>
        /// Vovida Software License v1.0
        /// </summary>
        [Description("Vovida Software License v1.0")] VSL_1_0,

        /// <summary>
        /// W3C Software Notice and License (2002-12-31)
        /// </summary>
        [Description("W3C Software Notice and License (2002-12-31)"), LicenseUri("https://www.w3.org/Consortium/Legal/2002/copyright-software-20021231.html"), Name("W3C")] W3C,

        /// <summary>
        /// W3C Software Notice and License (1998-07-20)
        /// </summary>
        [Description("W3C Software Notice and License (1998-07-20)"), LicenseUri("https://www.w3.org/Consortium/Legal/copyright-software-19980720.html"), Name("W3C-19980720")] W3C_19980720,

        /// <summary>
        /// W3C Software Notice and Document License (2015-05-13)
        /// </summary>
        [Description("W3C Software Notice and Document License (2015-05-13)"), LicenseUri("https://www.w3.org/Consortium/Legal/2015/copyright-software-and-document"), Name("W3C-20150513")] W3C_20150513,

        /// <summary>
        /// Sybase Open Watcom Public License 1.0
        /// </summary>
        [Description("Sybase Open Watcom Public License 1.0")] Watcom_1_0,

        /// <summary>
        /// Wsuipa License
        /// </summary>
        Wsuipa,

        /// <summary>
        /// Do What The F*ck You Want To Public License
        /// </summary>
        [Description("Do What The F*ck You Want To Public License"), LicenseUri("http://www.wtfpl.net/txt/copying/")] WTFPL,

        /// <summary>
        /// X11 License
        /// </summary>
        [LicenseUri("http://www.xfree86.org/3.3.6/COPYRIGHT2.html#3")] X11,

        /// <summary>
        /// Xerox License
        /// </summary>
        Xerox,

        /// <summary>
        /// XFree86 License 1.1
        /// </summary>
        [Description("XFree86 License 1.1"), LicenseUri("http://www.xfree86.org/current/LICENSE4.html")] XFree86_1_1,

        /// <summary>
        /// xinetd License
        /// </summary>
        xinetd,

        /// <summary>
        /// X.Net License
        /// </summary>
        [Description("X.Net License")] Xnet,

        /// <summary>
        /// XPP License
        /// </summary>
        [Description("XPP License")] xpp,

        /// <summary>
        /// XSkat License
        /// </summary>
        XSkat,

        /// <summary>
        /// Yahoo! Public License v1.0
        /// </summary>
        [Description("Yahoo! Public License v1.0")] YPL_1_0,

        /// <summary>
        /// Yahoo! Public License v1.1
        /// </summary>
        [Description("Yahoo! Public License v1.1")] YPL_1_1,

        /// <summary>
        /// Zed License
        /// </summary>
        Zed,

        /// <summary>
        /// Zend License v2.0
        /// </summary>
        [Description("Zend License v2.0")] Zend_2_0,

        /// <summary>
        /// Zimbra Public License v1.3
        /// </summary>
        [Description("Zimbra Public License v1.3")] Zimbra_1_3,

        /// <summary>
        /// Zimbra Public License v1.4
        /// </summary>
        [Description("Zimbra Public License v1.4"), LicenseUri("https://www.zimbra.com/legal/zimbra-public-license-1-4/")] Zimbra_1_4,

        /// <summary>
        /// zlib License
        /// </summary>
        [Description("zlib License"), LicenseUri("http://www.zlib.net/zlib_license.html")] Zlib,

        /// <summary>
        /// zlib/libpng License with Acknowledgement
        /// </summary>
        [Description("zlib/libpng License with Acknowledgement")] zlib_acknowledgement,

        /// <summary>
        /// Zope Public License 1.1
        /// </summary>
        [Description("Zope Public License 1.1"), LicenseUri("http://old.zope.org/Resources/License/ZPL-1.1")] ZPL_1_1,

        /// <summary>
        /// Zope Public License 2.0
        /// </summary>
        [Description("Zope Public License 2.0"), LicenseUri("http://old.zope.org/Resources/License/ZPL-2.0")] ZPL_2_0,

        /// <summary>
        /// Zope Public License 2.1
        /// </summary>
        [Description("Zope Public License 2.1"), LicenseUri("http://old.zope.org/Resources/ZPL/")] ZPL_2_1,
    }
}