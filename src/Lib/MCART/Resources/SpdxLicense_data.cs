﻿/*
SpdxLicense_data.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections.ObjectModel;
using static TheXDS.MCART.Resources.SpdxLicenseId;

namespace TheXDS.MCART.Resources;

public partial class SpdxLicense
{
    private record struct SpdxLicenseInfoItem(string? Description, string? LicenseUri = null, string? Name = null)
    {
        public static implicit operator SpdxLicenseInfoItem(string description) => new(description);
    }

    private static readonly ReadOnlyDictionary<SpdxLicenseId, SpdxLicenseInfoItem> licenseInfo = new Dictionary<SpdxLicenseId, SpdxLicenseInfoItem>
    {
        // las licencias que no estén en esta lista se generarán a partir del texto del Enum.
        {BSD0, new("BSD Zero Clause License", null, "0BSD")},
        {AAL, "Attribution Assurance License"},
        {Adobe_2006, "Adobe Systems Incorporated Source Code License Agreement"},
        {Adobe_Glyph, "Adobe Glyph List License" },
        {ADSL, "Amazon Digital Services License" },
        {AFL_1_1, "Academic Free License v1.1" },
        {AFL_1_2, "Academic Free License v1.2" },
        {AFL_2_0, "Academic Free License v2.0" },
        {AFL_2_1, "Academic Free License v2.1" },
        {AFL_3_0, "Academic Free License v3.0" },
        {AGPL_1_0_only, "Affero General Public License v1.0 only"},
        {AGPL_1_0_or_later, "Affero General Public License v1.0 or later"},
        {AGPL_3_0_only, new("GNU Affero General Public License v3.0 only", "https://www.gnu.org/licenses/agpl-3.0.txt")},
        {AGPL_3_0_or_later, new("GNU Affero General Public License v3.0 or later", "https://www.gnu.org/licenses/agpl-3.0.txt")},
        {Aladdin, "Aladdin Free Public License"},
        {AMDPLPA, new("AMD's plpa_map.c License")},
        {AML, "Apple MIT License"},
        {AMPAS, "Academy of Motion Picture Arts and Sciences BSD"},
        {ANTLR_PD, "ANTLR Software Rights Notice"},
        {Apache_1_0, new("Apache License 1.0", "https://www.apache.org/licenses/LICENSE-1.0")},
        {Apache_1_1, new("Apache License 1.1", "https://www.apache.org/licenses/LICENSE-1.1")},
        {Apache_2_0, new("Apache License 2.0", "http://www.apache.org/licenses/LICENSE-2.0.txt")},
        {APAFML, "Adobe Postscript AFM License"},
        {APL_1_0, "Adaptive Public License 1.0"},
        {APSL_1_0, "Apple Public Source License 1.0"},
        {APSL_1_1, "Apple Public Source License 1.1"},
        {APSL_1_2, "Apple Public Source License 1.2"},
        {APSL_2_0, new("Apple Public Source License 2.0", "https://opensource.apple.com/apsl/")},
        {Artistic_1_0, "Artistic License 1.0"},
        {Artistic_1_0_cl8, "Artistic License 1.0 w/clause 8"},
        {Artistic_1_0_Perl, new("Artistic License 1.0 (Perl)", "http://dev.perl.org/licenses/artistic.html" ) },
        {Artistic_2_0, new("Artistic License 2.0", "https://www.perlfoundation.org/artistic-license-20.html") },
        {BitTorrent_1_0, "BitTorrent Open Source License v1.0" },
        {BitTorrent_1_1, "BitTorrent Open Source License v1.1" },
        {Blessing, new("SQLite Blessing", "https://sqlite.org/src/raw/LICENSE.md?name=df5091916dbb40e6e9686186587125e1b2ff51f022cc334e886c19a0e9982724") },
        {BlueOak_1_0_0, new("Blue Oak Model License 1.0.0", "https://blueoakcouncil.org/license/1.0.0") },
        {BSD_1_Clause, new("BSD 1-Clause License","https://svnweb.freebsd.org/base/head/include/ifaddrs.h?revision=326823") },
        {BSD_2_Clause, "BSD 2-Clause \"Simplified\" License" },
        {BSD_2_Clause_FreeBSD, new("BSD 2-Clause FreeBSD License","https://www.freebsd.org/copyright/freebsd-license.html") },
        {BSD_2_Clause_NetBSD, new("BSD 2-Clause NetBSD License","http://www.netbsd.org/about/redistribution.html#default") },
        {BSD_2_Clause_Patent, "BSD-2-Clause Plus Patent License" },
        {BSD_3_Clause, "BSD 3-Clause \"New\" or \"Revised\" License"},
        {BSD_3_Clause_Attribution, "BSD with attribution"},
        {BSD_3_Clause_Clear, "BSD 3-Clause Clear License"},
        {BSD_3_Clause_LBNL, new("Lawrence Berkeley National Labs BSD variant license", null, "BSD-3-Clause-LBNL") },
        {BSD_3_Clause_No_Nuclear_License, new("BSD 3-Clause No Nuclear License",null,"BSD-3-Clause-No-Nuclear-License") },
        {BSD_3_Clause_No_Nuclear_License_2014, new("BSD 3-Clause No Nuclear License 2014",null, "BSD-3-Clause-No-Nuclear-License-2014") },
        {BSD_3_Clause_No_Nuclear_Warranty,new("BSD 3-Clause No Nuclear Warranty",null, "BSD-3-Clause-No-Nuclear-Warranty") },
        {BSD_3_Clause_Open_MPI, new("BSD 3-Clause Open MPI variant",null,"BSD-3-Clause-Open-MPI") },
        {BSD_4_Clause,new("BSD 4-Clause \"Original\" or \"Old\" License",null,"BSD-4-Clause") },
        {BSD_4_Clause_UC,new("BSD-4-Clause (University of California-Specific)","https://www.freebsd.org/copyright/license.html","BSD-4-Clause-UC") },
        {BSD_Protection, "BSD Protection License"},
        {BSD_Source_Code, "BSD Source Code Attribution"},
        {BSL_1_0, new("Boost Software License 1.0","https://www.boost.org/LICENSE_1_0.txt","BSL-1.0")},
        {bzip2_1_0_5, new("bzip2 and libbzip2 License v1.0.5", null, "bzip2-1.0.5") },
        {bzip2_1_0_6, new("bzip2 and libbzip2 License v1.0.6", null, "bzip2-1.0.6") },
        {CATOSL_1_1, new("Computer Associates Trusted Open Source License 1.1")},
        {CC_BY_1_0, new("Creative Commons Attribution 1.0 Generic","https://creativecommons.org/licenses/by/1.0/legalcode") },
        {CC_BY_2_0, new("Creative Commons Attribution 2.0 Generic","https://creativecommons.org/licenses/by/2.0/legalcode") },
        {CC_BY_2_5, new("Creative Commons Attribution 2.5 Generic","https://creativecommons.org/licenses/by/2.5/legalcode")},
        {CC_BY_3_0, new("Creative Commons Attribution 3.0 Unported","https://creativecommons.org/licenses/by/3.0/legalcode") },
        {CC_BY_4_0, new("Creative Commons Attribution 4.0 International","https://creativecommons.org/licenses/by/4.0/legalcode") },
        {CC_BY_NC_1_0, new("Creative Commons Attribution Non Commercial 1.0 Generic","https://creativecommons.org/licenses/by-nc/1.0/legalcode")},
        {CC_BY_NC_2_0, new("Creative Commons Attribution Non Commercial 2.0 Generic","https://creativecommons.org/licenses/by-nc/2.0/legalcode") },
        {CC_BY_NC_2_5, new("Creative Commons Attribution Non Commercial 2.5 Generic","https://creativecommons.org/licenses/by-nc/2.5/legalcode") },
        {CC_BY_NC_3_0, new("Creative Commons Attribution Non Commercial 3.0 Unported","https://creativecommons.org/licenses/by-nc/3.0/legalcode") },
        {CC_BY_NC_4_0, new("Creative Commons Attribution Non Commercial 4.0 International","https://creativecommons.org/licenses/by-nc/4.0/legalcode") },
        {CC_BY_NC_ND_1_0, new("Creative Commons Attribution Non Commercial No Derivatives 1.0 Generic","https://creativecommons.org/licenses/by-nc-nd/1.0/legalcode") },
        {CC_BY_NC_ND_2_0, new("Creative Commons Attribution Non Commercial No Derivatives 2.0 Generic","https://creativecommons.org/licenses/by-nc-nd/2.0/legalcode") },
        {CC_BY_NC_ND_2_5, new("Creative Commons Attribution Non Commercial No Derivatives 2.5 Generic","https://creativecommons.org/licenses/by-nc-nd/2.5/legalcode") },
        {CC_BY_NC_ND_3_0, new("Creative Commons Attribution Non Commercial No Derivatives 3.0 Unported","https://creativecommons.org/licenses/by-nc-nd/3.0/legalcode") },
        {CC_BY_NC_ND_4_0, new("Creative Commons Attribution Non Commercial No Derivatives 4.0 International","https://creativecommons.org/licenses/by-nc-nd/4.0/legalcode") },
        {CC_BY_NC_SA_1_0, new("Creative Commons Attribution Non Commercial Share Alike 1.0 Generic","https://creativecommons.org/licenses/by-nc-sa/1.0/legalcode") },
        {CC_BY_NC_SA_2_0, new("Creative Commons Attribution Non Commercial Share Alike 2.0 Generic","https://creativecommons.org/licenses/by-nc-sa/2.0/legalcode") },
        {CC_BY_NC_SA_2_5, new("Creative Commons Attribution Non Commercial Share Alike 2.5 Generic","https://creativecommons.org/licenses/by-nc-sa/2.5/legalcode") },
        {CC_BY_NC_SA_3_0, new("Creative Commons Attribution Non Commercial Share Alike 3.0 Unported","https://creativecommons.org/licenses/by-nc-sa/3.0/legalcode") },
        {CC_BY_NC_SA_4_0, new("Creative Commons Attribution Non Commercial Share Alike 4.0 International","https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode") },
        {CC_BY_ND_1_0, new("Creative Commons Attribution No Derivatives 1.0 Generic","https://creativecommons.org/licenses/by-nd/1.0/legalcode") },
        {CC_BY_ND_2_0, new("Creative Commons Attribution No Derivatives 2.0 Generic","https://creativecommons.org/licenses/by-nd/2.0/legalcode") },
        {CC_BY_ND_2_5, new("Creative Commons Attribution No Derivatives 2.5 Generic","https://creativecommons.org/licenses/by-nd/2.5/legalcode") },
        {CC_BY_ND_3_0, new("Creative Commons Attribution No Derivatives 3.0 Unported","https://creativecommons.org/licenses/by-nd/3.0/legalcode") },
        {CC_BY_ND_4_0, new("Creative Commons Attribution No Derivatives 4.0 International","https://creativecommons.org/licenses/by-nd/4.0/legalcode") },
        {CC_BY_SA_1_0, new("Creative Commons Attribution Share Alike 1.0 Generic","https://creativecommons.org/licenses/by-sa/1.0/legalcode") },
        {CC_BY_SA_2_0, new("Creative Commons Attribution Share Alike 2.0 Generic","https://creativecommons.org/licenses/by-sa/2.0/legalcode") },
        {CC_BY_SA_2_5, new("Creative Commons Attribution Share Alike 2.5 Generic","https://creativecommons.org/licenses/by-sa/2.5/legalcode") },
        {CC_BY_SA_3_0, new("Creative Commons Attribution Share Alike 3.0 Unported","https://creativecommons.org/licenses/by-sa/3.0/legalcode") },
        {CC_BY_SA_4_0, new("Creative Commons Attribution Share Alike 4.0 International","https://creativecommons.org/licenses/by-sa/4.0/legalcode") },
        {CC_PDDC, new("Creative Commons Public Domain Dedication and Certification","https://creativecommons.org/licenses/publicdomain/") },
        {CC0_1_0, new("Creative Commons Zero v1.0 Universal","https://creativecommons.org/publicdomain/zero/1.0/legalcode") },
        {CDDL_1_0, new("Common Development and Distribution License 1.0") },
        {CDDL_1_1, new("Common Development and Distribution License 1.1") },
        {CDLA_Permissive_1_0, new("Community Data License Agreement Permissive 1.0","https://cdla.io/permissive-1-0/") },
        {CDLA_Sharing_1_0, new("Community Data License Agreement Sharing 1.0","https://cdla.io/sharing-1-0/") },
        {CECILL_1_0, new("CeCILL Free Software License Agreement v1.0") },
        {CECILL_1_1, new("CeCILL Free Software License Agreement v1.1") },
        {CECILL_2_0, new("CeCILL Free Software License Agreement v2.0") },
        {CECILL_2_1, new("CeCILL Free Software License Agreement v2.1","https://cecill.info/licences/Licence_CeCILL_V2.1-en.html") },
        {CECILL_B, new("CeCILL-B Free Software License Agreement") },
        {CECILL_C, new("CeCILL-C Free Software License Agreement") },
        {CERN_OHL_1_1, new("CERN Open Hardware Licence v1.1") },
        {CERN_OHL_1_2, new("CERN Open Hardware Licence v1.2") },
        {ClArtistic, new("Clarified Artistic License", "https://www.ncftp.com/ncftp/doc/LICENSE.txt") },
        {CNRI_Jython, new("CNRI Jython License") },
        {CNRI_Python, new("CNRI Python License") },
        {CNRI_Python_GPL_Compatible, new("CNRI Python Open Source GPL Compatible License Agreement") },
        {Condor_1_1, new("Condor Public License v1.1") },
        {copyleft_next_0_3_0, new("copyleft-next 0.3.0","https://raw.githubusercontent.com/copyleft-next/copyleft-next/master/Releases/copyleft-next-0.3.0") },
        {copyleft_next_0_3_1, new("copyleft-next 0.3.1","https://raw.githubusercontent.com/copyleft-next/copyleft-next/master/Releases/copyleft-next-0.3.1") },
        {CPAL_1_0, new("Common Public Attribution License 1.0",null,"CPAL-1.0") },
        {CPL_1_0, new("Common Public License 1.0") },
        {CPOL_1_02, new("Code Project Open License 1.02") },
        {CUA_OPL_1_0, new("CUA Office Public License v1.0") },
        {curl, new(null,"https://raw.githubusercontent.com/curl/curl/master/COPYING")},
        {D_FSL_1_0, new("Deutsche Freie Software Lizenz","https://www.hbz-nrw.de/produkte/open-access/lizenzen/dfsl") },
        {ECL_1_0, new("Educational Community License v1.0") },
        {ECL_2_0, new("Educational Community License v2.0") },
        {EFL_1_0, new("Eiffel Forum License v1.0") },
        {EFL_2_0, new("Eiffel Forum License v2.0","http://www.eiffel-nice.org/license/eiffel-forum-license-2.html") },
        {eGenix, new("eGenix.com Public License 1.1.0","http://www.egenix.com/products/eGenix.com-Public-License-1.1.0.pdf") },
        {Entessa, new("Entessa Public License v1.0") },
        {EPL_1_0, new("Eclipse Public License 1.0","https://www.eclipse.org/legal/epl-v10.html") },
        {EPL_2_0, new("Eclipse Public License 2.0","https://www.eclipse.org/legal/epl-2.0/") },
        {ErlPL_1_1, new("Erlang Public License v1.1","https://www.erlang.org/EPLICENSE") },
        {etalab_2_0, new("Etalab Open License 2.0","https://raw.githubusercontent.com/DISIC/politique-de-contribution-open-source/master/LICENSE") },
        {EUDatagrid, new("EU DataGrid Software License") },
        {EUPL_1_0, new ("European Union Public License 1.0","https://ec.europa.eu/idabc/en/document/7330.html") },
        {EUPL_1_1, new("European Union Public License 1.1","https://joinup.ec.europa.eu/collection/eupl/eupl-text-11-12") },
        {EUPL_1_2, new("European Union Public License 1.2","https://joinup.ec.europa.eu/collection/eupl/eupl-text-11-12") },
        {Frameworx_1_0, new("Frameworx Open License 1.0") },
        {FreeImage, new ("FreeImage Public License v1.0","http://freeimage.sourceforge.net/freeimage-license.txt") },
        {FSFAP, new("FSF All Permissive License") },
        {FSFUL, new("FSF Unlimited License") },
        {FSFULLR, new("FSF Unlimited License (with License Retention)") },
        {FTL, new("Freetype Project License","http://git.savannah.gnu.org/cgit/freetype/freetype2.git/plain/docs/FTL.TXT") },
        {GFDL_1_1_only, new("GNU Free Documentation License v1.1 only","https://www.gnu.org/licenses/old-licenses/fdl-1.1.txt") },
        {GFDL_1_1_or_later, new("GNU Free Documentation License v1.1 or later","https://www.gnu.org/licenses/old-licenses/fdl-1.1.txt") },
        {GFDL_1_2_only, new("GNU Free Documentation License v1.2 only","https://www.gnu.org/licenses/old-licenses/fdl-1.2.txt") },
        {GFDL_1_2_or_later, new("GNU Free Documentation License v1.2 or later","https://www.gnu.org/licenses/old-licenses/fdl-1.2.txt") },
        {GFDL_1_3_only, new("GNU Free Documentation License v1.3 only","https://www.gnu.org/licenses/fdl-1.3.txt") },
        {GFDL_1_3_or_later, new("GNU Free Documentation License v1.3 or later","https://www.gnu.org/licenses/fdl-1.3.txt") },
        {Giftware, new(null, "https://liballeg.org/license.html") },
        {GL2PS, new(null, "http://www.geuz.org/gl2ps/COPYING.GL2PS") },
        {Glide, new("3dfx Glide License","http://wenchy.net/old/glidexp/COPYING.txt") },
        {GPL_1_0_only, new("GNU General Public License v1.0 only","https://www.gnu.org/licenses/old-licenses/gpl-1.0.txt") },
        {GPL_1_0_or_later, new("GNU General Public License v1.0 or later","https://www.gnu.org/licenses/old-licenses/gpl-1.0.txt") },
        {GPL_2_0_only, new("GNU General Public License v2.0 only","https://www.gnu.org/licenses/old-licenses/gpl-2.0.txt") },
        {GPL_2_0_or_later, new("GNU General Public License v2.0 or later","https://www.gnu.org/licenses/old-licenses/gpl-2.0.txt") },
        {GPL_3_0_only, new("GNU General Public License v3.0 only","https://www.gnu.org/licenses/gpl-3.0.txt") },
        {GPL_3_0_or_later, new("GNU General Public License v3.0 or later","https://www.gnu.org/licenses/gpl-3.0.txt") },
        {gSOAP_1_3b, new("gSOAP Public License v1.3b","http://www.cs.fsu.edu/~engelen/license.html","gSOAP-1.3b") },
        {HaskellReport, new("Haskell Language Report License") },
        {HPND, new("Historical Permission Notice and Disclaimer") },
        {HPND_sell_variant, new("Historical Permission Notice and Disclaimer - sell variant") },
        {IBM_pibs, new("IBM PowerPC Initialization and Boot Software") },
        {IJG, new("Independent JPEG Group License","https://dev.w3.org/cvsweb/Amaya/libjpeg/Attic/README?rev=1.2;content-type=text%2Fplain") },
        {ImageMagick, new("ImageMagick License","http://www.imagemagick.org/script/license.php") },
        {iMatix, new("iMatix Standard Function Library Agreement") },
        {Imlib2, new(null, "https://git.enlightenment.org/legacy/imlib2.git/plain/COPYING") },
        {Info_ZIP, new(null, "http://infozip.sourceforge.net/license.html") },
        {Intel, new("Intel Open Source License", "https://opensource.org/license/Intel") },
        {Intel_ACPI, new("Intel ACPI Software License Agreement") },
        {Interbase_1_0, new("Interbase Public License v1.0") },
        {IPA, new("IPA Font License") },
        {IPL_1_0, new("IBM Public License v1.0") },
        {ISC, new(null, "https://gitlab.isc.org/isc-projects/bind9/raw/master/COPYRIGHT") },
        {JasPer_2_0, new("JasPer License","https://www.ece.uvic.ca/~frodo/jasper/LICENSE") },
        {JPNIC, new("Japan Network Information Center License") },
        {JSON, new(null, "http://www.json.org/license.html") },
        {LAL_1_2, new("Licence Art Libre 1.2","http://artlibre.org/licence/lal/licence-art-libre-12/") },
        {LAL_1_3, new("Licence Art Libre 1.3","https://artlibre.org/") },
        {LGPL_2_0_only, new("GNU Library General Public License v2 only","https://www.gnu.org/licenses/old-licenses/lgpl-2.0.txt") },
        {LGPL_2_0_or_later, new("GNU Library General Public License v2 or later","https://www.gnu.org/licenses/old-licenses/lgpl-2.0.txt") },
        {LGPL_2_1_only, new("GNU Lesser General Public License v2.1 only","https://www.gnu.org/licenses/old-licenses/lgpl-2.1.txt") },
        {LGPL_2_1_or_later, new("GNU Lesser General Public License v2.1 or later","https://www.gnu.org/licenses/old-licenses/lgpl-2.0.txt") },
        {LGPL_3_0_only, new("GNU Lesser General Public License v3.0 only","https://www.gnu.org/licenses/lgpl-3.0.txt") },
        {LGPL_3_0_or_later, new("GNU Lesser General Public License v3.0 or later","https://www.gnu.org/licenses/lgpl-3.0.txt") },
        {LGPLLR, new("Lesser General Public License For Linguistic Resources","https://raw.githubusercontent.com/UnitexGramLab/LGPLLR/master/LGPLLR") },
        {Libpng, new(null, "http://www.libpng.org/pub/png/src/libpng-LICENSE.txt") },
        {libpng_2_0, new("PNG Reference Library version 2","http://www.libpng.org/pub/png/src/libpng-LICENSE.txt") },
        {LiLiQ_P_1_1, new("Licence Libre du Québec – Permissive version 1.1","https://forge.gouv.qc.ca/licence/liliq-v1-1/") },
        {LiLiQ_R_1_1, new("Licence Libre du Québec – Réciprocité version 1.1","https://forge.gouv.qc.ca/licence/liliq-v1-1/#réciprocité-liliq-r") },
        {LiLiQ_Rplus_1_1, new("Licence Libre du Québec – Réciprocité forte version 1.1","https://forge.gouv.qc.ca/licence/liliq-v1-1/#réciprocité-forte-liliq-r") },
        {Linux_OpenIB, new("Linux Kernel Variant of OpenIB.org license") },
        {LPL_1_0, new("Lucent Public License Version 1.0") },
        {LPL_1_02, new("Lucent Public License v1.02") },
        {LPPL_1_0, new("LaTeX Project Public License v1.0","https://www.latex-project.org/lppl/lppl-1-0.txt") },
        {LPPL_1_1, new("LaTeX Project Public License v1.1","https://www.latex-project.org/lppl/lppl-1-1.txt") },
        {LPPL_1_2, new("LaTeX Project Public License v1.2","https://www.latex-project.org/lppl/lppl-1-2.txt") },
        {LPPL_1_3a, new("LaTeX Project Public License v1.3a","https://www.latex-project.org/lppl/lppl-1-3a.txt","LPPL-1.3a") },
        {LPPL_1_3c, new("LaTeX Project Public License v1.3c","https://www.latex-project.org/lppl/lppl-1-3c.txt","LPPL-1.3c") },
        {MIT, new(null, "https://mit-license.org/") },
        {MIT_0, new("MIT No Attribution") },
        {MIT_advertising, new("Enlightenment License (e16)") },
        {MIT_CMU, new("CMU License") },
        {MIT_enna, new("enna License") },
        {MIT_feh, new("feh License") },
        {MITNFA, "MIT +no-false-attribs license" },
        {MPL_1_0, new("Mozilla Public License 1.0","https://website-archive.mozilla.org/www.mozilla.org/mpl/mpl/1.0/") },
        {MPL_1_1, new("Mozilla Public License 1.1","https://www.mozilla.org/media/MPL/1.1/index.0c5913925d40.txt","MPL-1.1") },
        {MPL_2_0, new("Mozilla Public License 2.0","https://www.mozilla.org/en-US/MPL/2.0/","MPL-2.0") },
        {MPL_2_0_no_copyleft_exception, new("Mozilla Public License 2.0 (no copyleft exception)","https://www.mozilla.org/en-US/MPL/2.0/") },
        {MS_PL, new("Microsoft Public License", "https://licenses.nuget.org/MS-PL") },
        {MS_RL, new ("Microsoft Reciprocal License", "https://licenses.nuget.org/MS-RL") },
        {MTLL, new("Matrix Template Library License") },
        {MulanPSL_1_0, new("Mulan Permissive Software License, Version 1","https://license.coscl.org.cn/MulanPSL/") },
        {NASA_1_3, new("NASA Open Source Agreement 1.3","https://ti.arc.nasa.gov/opensource/nosa/") },
        {Naumen, new("Naumen Public License") },
        {NBPL_1_0, "Net Boolean Public License v1" },
        {NCSA, new("University of Illinois/NCSA Open Source License") },
        {Net_SNMP, new(null, "http://net-snmp.sourceforge.net/about/license.html") },
        {NetCDF, new(null, "https://www.unidata.ucar.edu/software/netcdf/copyright.html") },
        {NGPL, "Nethack General Public License" },
        {NLOD_1_0, new("Norwegian Licence for Open Government Data","http://data.norge.no/nlod/en/1.0") },
        {NLPL, "No Limit Public License"},
        {Nokia, "Nokia Open Source License"},
        {NOSL, new("Netizen Open Source License") },
        {NPL_1_0, new("Netscape Public License v1.0","https://website-archive.mozilla.org/www.mozilla.org/mpl/mpl/npl/1.0/") },
        {NPL_1_1, new("Netscape Public License v1.1","https://website-archive.mozilla.org/www.mozilla.org/mpl/mpl/npl/1.1/") },
        {NPOSL_3_0, "Non-Profit Open Software License 3.0"},
        {NRL, new(null, "http://web.mit.edu/network/isakmp/nrllicense.html") },
        {OCCT_PL, new("Open CASCADE Technology Public License","https://www.opencascade.com/content/occt-public-license") },
        {OCLC_2_0, "OCLC Research Public License 2.0"},
        {ODbL_1_0, new("ODC Open Database License v1.0","https://www.opendatacommons.org/licenses/odbl/1.0/") },
        {ODC_By_1_0, new("Open Data Commons Attribution License v1.0","https://opendatacommons.org/files/2018/02/odc_by_1.0_public_text.txt") },
        {OFL_1_0, new("SIL Open Font License 1.0","https://scripts.sil.org/OFL10_web") },
        {OFL_1_1, new("SIL Open Font License 1.1","https://scripts.sil.org/OFL_web") },
        {OGL_Canada_2_0, new("Open Government Licence - Canada","https://open.canada.ca/en/open-government-licence-canada") },
        {OGL_UK_1_0, new("Open Government Licence v1.0","http://www.nationalarchives.gov.uk/doc/open-government-licence/version/1/") },
        {OGL_UK_2_0, new("Open Government Licence v2.0","http://www.nationalarchives.gov.uk/doc/open-government-licence/version/2/") },
        {OGL_UK_3_0, new("Open Government Licence v3.0","http://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/") },
        {OGTSL, new("Open Group Test Suite License","http://www.opengroup.org/testing/downloads/The_Open_Group_TSL.txt") },
        {OLDAP_1_1, "Open LDAP Public License v1.1"},
        {OLDAP_1_2, "Open LDAP Public License v1.2"},
        {OLDAP_1_3, "Open LDAP Public License v1.3"},
        {OLDAP_1_4, "Open LDAP Public License v1.4"},
        {OLDAP_2_0, "Open LDAP Public License v2.0"},
        {OLDAP_2_0_1, "Open LDAP Public License v2.0.1"},
        {OLDAP_2_1, "Open LDAP Public License v2.1"},
        {OLDAP_2_2, "Open LDAP Public License v2.2"},
        {OLDAP_2_2_1, "Open LDAP Public License v2.2.1"},
        {OLDAP_2_2_2, "Open LDAP Public License 2.2.2"},
        {OLDAP_2_3, "Open LDAP Public License v2.3"},
        {OLDAP_2_4, "Open LDAP Public License v2.4"},
        {OLDAP_2_5, "Open LDAP Public License v2.5"},
        {OLDAP_2_6, "Open LDAP Public License v2.6"},
        {OLDAP_2_7, "Open LDAP Public License v2.7"},
        {OLDAP_2_8, new("Open LDAP Public License v2.8","http://www.openldap.org/software/release/license.html") },
        {OML, "Open Market License"},
        {OpenSSL, new("OpenSSL License","https://www.openssl.org/source/license.html") },
        {OPL_1_0, "Open Public License v1.0"},
        {OSET_PL_2_1, new("OSET Public License version 2.1","https://www.osetfoundation.org/public-license") },
        {OSL_1_0, "Open Software License 1.0"},
        {OSL_1_1, "Open Software License 1.1"},
        {OSL_2_0, "Open Software License 2.0"},
        {OSL_2_1, "Open Software License 2.1"},
        {OSL_3_0, "Open Software License 3.0"},
        {Parity_6_0_0, new("The Parity Public License 6.0.0","https://paritylicense.com/versions/6.0.0.html") },
        {PDDL_1_0, new("ODC Public Domain Dedication & License 1.0","https://opendatacommons.org/licenses/pddl/1.0/") },
        {PHP_3_0, new("PHP License v3.0","https://www.php.net/license/3_0.txt") },
        {PHP_3_01, new("PHP License v3.01","https://www.php.net/license/3_01.txt") },
        {Plexus, "Plexus Classworlds License"},
        {PostgreSQL, new("PostgreSQL License","https://www.postgresql.org/about/licence/") },
        {Python_2_0, new("Python License 2.0", "https://www.python.org/download/releases/2.0/license/") },
        {QPL_1_0, "Q Public License 1.0"},
        {RHeCos_1_1, new("Red Hat eCos Public License v1.1","http://ecos.sourceware.org/old-license.html") },
        {RPL_1_1, "Reciprocal Public License 1.1"},
        {RPL_1_5, "Reciprocal Public License 1.5"},
        {RPSL_1_0, "RealNetworks Public Source License v1.0"},
        {RSA_MD, "RSA Message-Digest License"},
        {RSCPL, "Ricoh Source Code Public License"},
        {Ruby, new(null, "http://www.ruby-lang.org/en/about/license.txt") },
        {SAX_PD, new("Sax Public Domain Notice","http://www.saxproject.org/copying.html") },
        {SCEA, "SCEA Shared Source License"},
        {Sendmail, new(null, "http://www.sendmail.com/pdfs/open_source/sendmail_license.pdf") },
        {Sendmail_8_23, new("Sendmail License 8.23","https://www.proofpoint.com/sites/default/files/sendmail-license.pdf") },
        {SGI_B_1_0, "SGI Free Software License B v1.0"},
        {SGI_B_1_1, "SGI Free Software License B v1.1"},
        {SGI_B_2_0, "SGI Free Software License B v2.0"},
        {SHL_0_5, new("Solderpad Hardware License v0.5","https://solderpad.org/licenses/SHL-0.5/") },
        {SHL_0_51, new("Solderpad Hardware License, Version 0.51","https://solderpad.org/licenses/SHL-0.51/") },
        {SimPL_2_0, "Simple Public License 2.0"},
        {SISSL, new("Sun Industry Standards Source License v1.1","http://www.openoffice.org/licenses/sissl_license.html") },
        {SISSL_1_2, new("Sun Industry Standards Source License v1.2","http://gridscheduler.sourceforge.net/Gridengine_SISSL_license.html") },
        {SMLNJ, new("Standard ML of New Jersey License","https://www.smlnj.org/license.html") },
        {SMPPL, new("Secure Messaging Protocol Public License","https://raw.githubusercontent.com/dcblake/SMP/master/Documentation/License.txt") },
        {SNIA, new("SNIA Public License 1.1") },
        {Spencer_86, new("Spencer License 86",null,"Spencer-86") },
        {Spencer_94, new("Spencer License 94",null,"Spencer-94") },
        {Spencer_99, new("Spencer License 99",null,"Spencer-99") },
        {SPL_1_0, "Sun Public License v1.0"},
        {SSH_OpenSSH, "SSH OpenSSH license"},
        {SSH_short, "SSH short notice"},
        {SSPL_1_0, new("Server Side Public License, v 1","https://www.mongodb.com/licensing/server-side-public-license") },
        {SugarCRM_1_1_3, "SugarCRM Public License v1.1.3"},
        {SWL, "Scheme Widget Library (SWL) Software License Agreement"},
        {TAPR_OHL_1_0, new("TAPR Open Hardware License v1.0","https://www.tapr.org/TAPR_Open_Hardware_License_v1.0.txt") },
        {TCL, new("TCL/TK License","http://www.tcl.tk/software/tcltk/license.html") },
        {TCP_wrappers, "TCP Wrappers License"},
        {TMate, new("TMate Open Source License","https://svnkit.com/license.html") },
        {TORQUE_1_1, "TORQUE v2.5+ Software License v1.1"},
        {TOSL, "Trusster Open Source License"},
        {TU_Berlin_1_0, "Technische Universitaet Berlin License 1.0"},
        {TU_Berlin_2_0, "Technische Universitaet Berlin License 2.0"},
        {UCL_1_0, "Upstream Compatibility License v1.0"},
        {Unicode_DFS_2015, new("Unicode License Agreement - Data Files and Software (2015)","http://www.unicode.org/copyright.html") },
        {Unicode_DFS_2016, new("Unicode License Agreement - Data Files and Software (2016)","http://www.unicode.org/copyright.html") },
        {Unicode_TOU, new("Unicode Terms of Use","http://www.unicode.org/copyright.html") },
        {Unlicense, new("The Unlicense","https://unlicense.org/") },
        {UPL_1_0, "Universal Permissive License v1.0"},
        {Vim, new(null, "http://vimdoc.sourceforge.net/htmldoc/uganda.html") },
        {VOSTROM, "VOSTROM Public License for Open Source"},
        {VSL_1_0, "Vovida Software License v1.0"},
        {W3C, new("W3C Software Notice and License (2002-12-31)","https://www.w3.org/Consortium/Legal/2002/copyright-software-20021231.html","W3C") },
        {W3C_19980720, new("W3C Software Notice and License (1998-07-20)","https://www.w3.org/Consortium/Legal/copyright-software-19980720.html","W3C-19980720") },
        {W3C_20150513, new("W3C Software Notice and Document License (2015-05-13)","https://www.w3.org/Consortium/Legal/2015/copyright-software-and-document","W3C-20150513") },
        {Watcom_1_0, "Sybase Open Watcom Public License 1.0"},
        {WTFPL, new("Do What The F*ck You Want To Public License","http://www.wtfpl.net/txt/copying/") },
        {X11, new(null, "http://www.xfree86.org/3.3.6/COPYRIGHT2.html#3") },
        {XFree86_1_1, new("XFree86 License 1.1","http://www.xfree86.org/current/LICENSE4.html") },
        {Xnet, "X.Net License"},
        {xpp, "XPP License"},
        {YPL_1_0, "Yahoo! Public License v1.0"},
        {YPL_1_1, new("Yahoo! Public License v1.1","https://www.zimbra.com/license/yahoo_public_license_1.1.html") },
        {Zend_2_0, "Zend License v2.0"},
        {Zimbra_1_3, "Zimbra Public License v1.3"},
        {Zimbra_1_4, new("Zimbra Public License v1.4","https://www.zimbra.com/legal/zimbra-public-license-1-4/") },
        {Zlib, new("zlib License","http://www.zlib.net/zlib_license.html") },
        {zlib_acknowledgement, "zlib/libpng License with Acknowledgement"},
        {ZPL_1_1, new("Zope Public License 1.1","http://old.zope.org/Resources/License/ZPL-1.1") },
        {ZPL_2_0, new("Zope Public License 2.0","http://old.zope.org/Resources/License/ZPL-2.0") },
        {ZPL_2_1, new("Zope Public License 2.1","http://old.zope.org/Resources/ZPL/") },
    }.AsReadOnly();
}