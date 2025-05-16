/*
SpdxLicenseId.cs

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

namespace TheXDS.MCART.Resources;

/// <summary>
/// Enumerates all licenses registered within the Software Package Data
/// Exchange (SPDX) standards.
/// </summary>
public enum SpdxLicenseId
{
    /// <summary>
    /// BSD Zero Clause License
    /// </summary>
    BSD0,
    /// <summary>
    /// Attribution Assurance License
    /// </summary>
    AAL,
    /// <summary>
    /// Abstyles License
    /// </summary>
    Abstyles,
    /// <summary>
    /// Adobe Systems Incorporated Source Code License Agreement
    /// </summary>
    Adobe_2006,
    /// <summary>
    /// Adobe Glyph List License
    /// </summary>
    Adobe_Glyph,
    /// <summary>
    /// Amazon Digital Services License
    /// </summary>
    ADSL,
    /// <summary>
    /// Academic Free License v1.1
    /// </summary>
    AFL_1_1,
    /// <summary>
    /// Academic Free License v1.2
    /// </summary>
    AFL_1_2,
    /// <summary>
    /// Academic Free License v2.0
    /// </summary>
    AFL_2_0,
    /// <summary>
    /// Academic Free License v2.1
    /// </summary>
    AFL_2_1,
    /// <summary>
    /// Academic Free License v3.0
    /// </summary>
    AFL_3_0,
    /// <summary>
    /// Afmparse License
    /// </summary>
    Afmparse,
    /// <summary>
    /// Affero General Public License v1.0 only
    /// </summary>
    AGPL_1_0_only,
    /// <summary>
    /// Affero General Public License v1.0 or later
    /// </summary>
    AGPL_1_0_or_later,
    /// <summary>
    /// GNU Affero General Public License v3.0 only
    /// </summary>
    AGPL_3_0_only,
    /// <summary>
    /// GNU Affero General Public License v3.0 or later
    /// </summary>
    AGPL_3_0_or_later,
    /// <summary>
    /// Aladdin Free Public License
    /// </summary>
    Aladdin,
    /// <summary>
    /// AMD's plpa_map.c License
    /// </summary>
    AMDPLPA,
    /// <summary>
    /// Apple MIT License
    /// </summary>
    AML,
    /// <summary>
    /// Academy of Motion Picture Arts and Sciences BSD
    /// </summary>
    AMPAS,
    /// <summary>
    /// ANTLR Software Rights Notice
    /// </summary>
    ANTLR_PD,
    /// <summary>
    /// Apache License 1.0
    /// </summary>
    Apache_1_0,
    /// <summary>
    /// Apache License 1.1
    /// </summary>
    Apache_1_1,
    /// <summary>
    /// Apache License 2.0
    /// </summary>
    Apache_2_0,
    /// <summary>
    /// Adobe Postscript AFM License
    /// </summary>
    APAFML,
    /// <summary>
    /// Adaptive Public License 1.0
    /// </summary>
    APL_1_0,
    /// <summary>
    /// Apple Public Source License 1.0
    /// </summary>
    APSL_1_0,
    /// <summary>
    /// Apple Public Source License 1.1
    /// </summary>
    APSL_1_1,
    /// <summary>
    /// Apple Public Source License 1.2
    /// </summary>
    APSL_1_2,
    /// <summary>
    /// Apple Public Source License 2.0
    /// </summary>
    APSL_2_0,
    /// <summary>
    /// Artistic License 1.0
    /// </summary>
    Artistic_1_0,
    /// <summary>
    /// Artistic License 1.0 w/clause 8
    /// </summary>
    Artistic_1_0_cl8,
    /// <summary>
    /// Artistic License 1.0 (Perl)
    /// </summary>
    Artistic_1_0_Perl,
    /// <summary>
    /// Artistic License 2.0
    /// </summary>
    Artistic_2_0,
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
    BitTorrent_1_0,
    /// <summary>
    /// BitTorrent Open Source License v1.1
    /// </summary>
    BitTorrent_1_1,
    /// <summary>
    /// SQLite Blessing
    /// </summary>
    Blessing,
    /// <summary>
    /// Blue Oak Model License 1.0.0
    /// </summary>
    BlueOak_1_0_0,
    /// <summary>
    /// Borceux license
    /// </summary>
    Borceux,
    /// <summary>
    /// BSD 1-Clause License
    /// </summary>
    BSD_1_Clause,
    /// <summary>
    /// BSD 2-Clause "Simplified" License
    /// </summary>
    BSD_2_Clause,
    /// <summary>
    /// BSD 2-Clause FreeBSD License
    /// </summary>
    BSD_2_Clause_FreeBSD,
    /// <summary>
    /// BSD 2-Clause NetBSD License
    /// </summary>
    BSD_2_Clause_NetBSD,
    /// <summary>
    /// BSD-2-Clause Plus Patent License
    /// </summary>
    BSD_2_Clause_Patent,
    /// <summary>
    /// BSD 3-Clause "New" or "Revised" License
    /// </summary>
    BSD_3_Clause,
    /// <summary>
    /// BSD with attribution
    /// </summary>
    BSD_3_Clause_Attribution,
    /// <summary>
    /// BSD 3-Clause Clear License
    /// </summary>
    BSD_3_Clause_Clear,
    /// <summary>
    /// Lawrence Berkeley National Labs BSD variant license
    /// </summary>
    BSD_3_Clause_LBNL,
    /// <summary>
    /// BSD 3-Clause No Nuclear License
    /// </summary>
    BSD_3_Clause_No_Nuclear_License,
    /// <summary>
    /// BSD 3-Clause No Nuclear License 2014
    /// </summary>
    BSD_3_Clause_No_Nuclear_License_2014,
    /// <summary>
    /// BSD 3-Clause No Nuclear Warranty
    /// </summary>
    BSD_3_Clause_No_Nuclear_Warranty,
    /// <summary>
    /// BSD 3-Clause Open MPI variant
    /// </summary>
    BSD_3_Clause_Open_MPI,
    /// <summary>
    /// BSD 4-Clause "Original" or "Old" License
    /// </summary>
    BSD_4_Clause,
    /// <summary>
    /// BSD-4-Clause (University of California-Specific)
    /// </summary>
    BSD_4_Clause_UC,
    /// <summary>
    /// BSD Protection License
    /// </summary>
    BSD_Protection,
    /// <summary>
    /// BSD Source Code Attribution
    /// </summary>
    BSD_Source_Code,
    /// <summary>
    /// Boost Software License 1.0
    /// </summary>
    BSL_1_0,
    /// <summary>
    /// bzip2 and libbzip2 License v1.0.5
    /// </summary>
    bzip2_1_0_5,
    /// <summary>
    /// bzip2 and libbzip2 License v1.0.6
    /// </summary>
    bzip2_1_0_6,
    /// <summary>
    /// Caldera License
    /// </summary>
    Caldera,
    /// <summary>
    /// Computer Associates Trusted Open Source License 1.1
    /// </summary>
    CATOSL_1_1,
    /// <summary>
    /// Creative Commons Attribution 1.0 Generic
    /// </summary>
    CC_BY_1_0,
    /// <summary>
    /// Creative Commons Attribution 2.0 Generic
    /// </summary>
    CC_BY_2_0,
    /// <summary>
    /// Creative Commons Attribution 2.5 Generic
    /// </summary>
    CC_BY_2_5,
    /// <summary>
    /// Creative Commons Attribution 3.0 Unported
    /// </summary>
    CC_BY_3_0,
    /// <summary>
    /// Creative Commons Attribution 4.0 International
    /// </summary>
    CC_BY_4_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial 1.0 Generic
    /// </summary>
    CC_BY_NC_1_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial 2.0 Generic
    /// </summary>
    CC_BY_NC_2_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial 2.5 Generic
    /// </summary>
    CC_BY_NC_2_5,
    /// <summary>
    /// Creative Commons Attribution Non Commercial 3.0 Unported
    /// </summary>
    CC_BY_NC_3_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial 4.0 International
    /// </summary>
    CC_BY_NC_4_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial No Derivatives 1.0 Generic
    /// </summary>
    CC_BY_NC_ND_1_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial No Derivatives 2.0 Generic
    /// </summary>
    CC_BY_NC_ND_2_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial No Derivatives 2.5 Generic
    /// </summary>
    CC_BY_NC_ND_2_5,
    /// <summary>
    /// Creative Commons Attribution Non Commercial No Derivatives 3.0 Unported
    /// </summary>
    CC_BY_NC_ND_3_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial No Derivatives 4.0 International
    /// </summary>
     CC_BY_NC_ND_4_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial Share Alike 1.0 Generic
    /// </summary>
    CC_BY_NC_SA_1_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial Share Alike 2.0 Generic
    /// </summary>
    CC_BY_NC_SA_2_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial Share Alike 2.5 Generic
    /// </summary>
    CC_BY_NC_SA_2_5,
    /// <summary>
    /// Creative Commons Attribution Non Commercial Share Alike 3.0 Unported
    /// </summary>
    CC_BY_NC_SA_3_0,
    /// <summary>
    /// Creative Commons Attribution Non Commercial Share Alike 4.0 International
    /// </summary>
    CC_BY_NC_SA_4_0,
    /// <summary>
    /// Creative Commons Attribution No Derivatives 1.0 Generic
    /// </summary>
    CC_BY_ND_1_0,
    /// <summary>
    /// Creative Commons Attribution No Derivatives 2.0 Generic
    /// </summary>
    CC_BY_ND_2_0,
    /// <summary>
    /// Creative Commons Attribution No Derivatives 2.5 Generic
    /// </summary>
    CC_BY_ND_2_5,
    /// <summary>
    /// Creative Commons Attribution No Derivatives 3.0 Unported
    /// </summary>
    CC_BY_ND_3_0,
    /// <summary>
    /// Creative Commons Attribution No Derivatives 4.0 International
    /// </summary>
    CC_BY_ND_4_0,
    /// <summary>
    /// Creative Commons Attribution Share Alike 1.0 Generic
    /// </summary>
    CC_BY_SA_1_0,
    /// <summary>
    /// Creative Commons Attribution Share Alike 2.0 Generic
    /// </summary>
    CC_BY_SA_2_0,
    /// <summary>
    /// Creative Commons Attribution Share Alike 2.5 Generic
    /// </summary>
    CC_BY_SA_2_5,
    /// <summary>
    /// Creative Commons Attribution Share Alike 3.0 Unported
    /// </summary>
    CC_BY_SA_3_0,
    /// <summary>
    /// Creative Commons Attribution Share Alike 4.0 International
    /// </summary>
    CC_BY_SA_4_0,
    /// <summary>
    /// Creative Commons Public Domain Dedication and Certification
    /// </summary>
    CC_PDDC,
    /// <summary>
    /// Creative Commons Zero v1.0 Universal
    /// </summary>
    CC0_1_0,
    /// <summary>
    /// Common Development and Distribution License 1.0
    /// </summary>
    CDDL_1_0,
    /// <summary>
    /// Common Development and Distribution License 1.1
    /// </summary>
    CDDL_1_1,
    /// <summary>
    /// Community Data License Agreement Permissive 1.0
    /// </summary>
    CDLA_Permissive_1_0,
    /// <summary>
    /// Community Data License Agreement Sharing 1.0
    /// </summary>
    CDLA_Sharing_1_0,
    /// <summary>
    /// CeCILL Free Software License Agreement v1.0
    /// </summary>
    CECILL_1_0,
    /// <summary>
    /// CeCILL Free Software License Agreement v1.1
    /// </summary>
    CECILL_1_1,
    /// <summary>
    /// CeCILL Free Software License Agreement v2.0
    /// </summary>
    CECILL_2_0,
    /// <summary>
    /// CeCILL Free Software License Agreement v2.1
    /// </summary>
    CECILL_2_1,
    /// <summary>
    /// CeCILL-B Free Software License Agreement
    /// </summary>
    CECILL_B,
    /// <summary>
    /// CeCILL-C Free Software License Agreement
    /// </summary>
    CECILL_C,
    /// <summary>
    /// CERN Open Hardware Licence v1.1
    /// </summary>
    CERN_OHL_1_1,
    /// <summary>
    /// CERN Open Hardware Licence v1.2
    /// </summary>
    CERN_OHL_1_2,
    /// <summary>
    /// Clarified Artistic License
    /// </summary>
    ClArtistic,
    /// <summary>
    /// CNRI Jython License
    /// </summary>
    CNRI_Jython,
    /// <summary>
    /// CNRI Python License
    /// </summary>
    CNRI_Python,
    /// <summary>
    /// CNRI Python Open Source GPL Compatible License Agreement
    /// </summary>
    CNRI_Python_GPL_Compatible,
    /// <summary>
    /// Condor Public License v1.1
    /// </summary>
    Condor_1_1,
    /// <summary>
    /// copyleft-next 0.3.0
    /// </summary>
    copyleft_next_0_3_0,
    /// <summary>
    /// copyleft-next 0.3.1
    /// </summary>
    copyleft_next_0_3_1,
    /// <summary>
    /// Common Public Attribution License 1.0
    /// </summary>
    CPAL_1_0,
    /// <summary>
    /// Common Public License 1.0
    /// </summary>
    CPL_1_0,
    /// <summary>
    /// Code Project Open License 1.02
    /// </summary>
    CPOL_1_02,
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
    CUA_OPL_1_0,
    /// <summary>
    /// Cube License
    /// </summary>
    Cube,
    /// <summary>
    /// curl License
    /// </summary>
    curl,
    /// <summary>
    /// Deutsche Freie Software Lizenz
    /// </summary>
    D_FSL_1_0,
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
    ECL_1_0,
    /// <summary>
    /// Educational Community License v2.0
    /// </summary>
    ECL_2_0,
    /// <summary>
    /// Eiffel Forum License v1.0
    /// </summary>
    EFL_1_0,
    /// <summary>
    /// Eiffel Forum License v2.0
    /// </summary>
    EFL_2_0,
    /// <summary>
    /// eGenix.com Public License 1.1.0
    /// </summary>
    eGenix,
    /// <summary>
    /// Entessa Public License v1.0
    /// </summary>
    Entessa,
    /// <summary>
    /// Eclipse Public License 1.0
    /// </summary>
    EPL_1_0,
    /// <summary>
    /// Eclipse Public License 2.0
    /// </summary>
    EPL_2_0,
    /// <summary>
    /// Erlang Public License v1.1
    /// </summary>
    ErlPL_1_1,
    /// <summary>
    /// Etalab Open License 2.0
    /// </summary>
    etalab_2_0,
    /// <summary>
    /// EU DataGrid Software License
    /// </summary>
    EUDatagrid,
    /// <summary>
    /// European Union Public License 1.0
    /// </summary>
    EUPL_1_0,
    /// <summary>
    /// European Union Public License 1.1
    /// </summary>
    EUPL_1_1,
    /// <summary>
    /// European Union Public License 1.2
    /// </summary>
    EUPL_1_2,
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
    Frameworx_1_0,
    /// <summary>
    /// FreeImage Public License v1.0
    /// </summary>
    FreeImage,
    /// <summary>
    /// FSF All Permissive License
    /// </summary>
    FSFAP,
    /// <summary>
    /// FSF Unlimited License
    /// </summary>
    FSFUL,
    /// <summary>
    /// FSF Unlimited License (with License Retention)
    /// </summary>
    FSFULLR,
    /// <summary>
    /// Freetype Project License
    /// </summary>
    FTL,
    /// <summary>
    /// GNU Free Documentation License v1.1 only
    /// </summary>
    GFDL_1_1_only,
    /// <summary>
    /// GNU Free Documentation License v1.1 or later
    /// </summary>
    GFDL_1_1_or_later,
    /// <summary>
    /// GNU Free Documentation License v1.2 only
    /// </summary>
    GFDL_1_2_only,
    /// <summary>
    /// GNU Free Documentation License v1.2 or later
    /// </summary>
    GFDL_1_2_or_later,
    /// <summary>
    /// GNU Free Documentation License v1.3 only
    /// </summary>
    GFDL_1_3_only,
    /// <summary>
    /// GNU Free Documentation License v1.3 or later
    /// </summary>
    GFDL_1_3_or_later,
    /// <summary>
    /// Giftware License
    /// </summary>
    Giftware,
    /// <summary>
    /// GL2PS License
    /// </summary>
    GL2PS,
    /// <summary>
    /// 3dfx Glide License
    /// </summary>
    Glide,
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
    GPL_1_0_only,
    /// <summary>
    /// GNU General Public License v1.0 or later
    /// </summary>
    GPL_1_0_or_later,
    /// <summary>
    /// GNU General Public License v2.0 only
    /// </summary>
    GPL_2_0_only,
    /// <summary>
    /// GNU General Public License v2.0 or later
    /// </summary>
    GPL_2_0_or_later,
    /// <summary>
    /// GNU General Public License v3.0 only
    /// </summary>
    GPL_3_0_only,
    /// <summary>
    /// GNU General Public License v3.0 or later
    /// </summary>
    GPL_3_0_or_later,
    /// <summary>
    /// gSOAP Public License v1.3b
    /// </summary>
    gSOAP_1_3b,
    /// <summary>
    /// Haskell Language Report License
    /// </summary>
    HaskellReport,
    /// <summary>
    /// Historical Permission Notice and Disclaimer
    /// </summary>
    HPND,
    /// <summary>
    /// Historical Permission Notice and Disclaimer - sell variant
    /// </summary>
    HPND_sell_variant,
    /// <summary>
    /// IBM PowerPC Initialization and Boot Software
    /// </summary>
    IBM_pibs,
    /// <summary>
    /// ICU License
    /// </summary>
    ICU,
    /// <summary>
    /// Independent JPEG Group License
    /// </summary>
    IJG,
    /// <summary>
    /// ImageMagick License
    /// </summary>
    ImageMagick,
    /// <summary>
    /// iMatix Standard Function Library Agreement
    /// </summary>
    iMatix,
    /// <summary>
    /// Imlib2 License
    /// </summary>
    Imlib2,
    /// <summary>
    /// Info-ZIP License
    /// </summary>
    Info_ZIP,
    /// <summary>
    /// Intel Open Source License
    /// </summary>
    Intel,
    /// <summary>
    /// Intel ACPI Software License Agreement
    /// </summary>
    Intel_ACPI,
    /// <summary>
    /// Interbase Public License v1.0
    /// </summary>
    Interbase_1_0,
    /// <summary>
    /// IPA Font License
    /// </summary>
    IPA,
    /// <summary>
    /// IBM Public License v1.0
    /// </summary>
    IPL_1_0,
    /// <summary>
    /// ISC License
    /// </summary>
    ISC,
    /// <summary>
    /// JasPer License
    /// </summary>
    JasPer_2_0,
    /// <summary>
    /// Japan Network Information Center License
    /// </summary>
    JPNIC,
    /// <summary>
    /// JSON License
    /// </summary>
    JSON,
    /// <summary>
    /// Licence Art Libre 1.2
    /// </summary>
    LAL_1_2,
    /// <summary>
    /// Licence Art Libre 1.3
    /// </summary>
    LAL_1_3,
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
    LGPL_2_0_only,
    /// <summary>
    /// GNU Library General Public License v2 or later
    /// </summary>
    LGPL_2_0_or_later,
    /// <summary>
    /// GNU Lesser General Public License v2.1 only
    /// </summary>
    LGPL_2_1_only,
    /// <summary>
    /// GNU Lesser General Public License v2.1 or later
    /// </summary>
    LGPL_2_1_or_later,
    /// <summary>
    /// GNU Lesser General Public License v3.0 only
    /// </summary>
    LGPL_3_0_only,
    /// <summary>
    /// GNU Lesser General Public License v3.0 or later
    /// </summary>
    LGPL_3_0_or_later,
    /// <summary>
    /// Lesser General Public License For Linguistic Resources
    /// </summary>
    LGPLLR,
    /// <summary>
    /// libpng License
    /// </summary>
    Libpng,
    /// <summary>
    /// PNG Reference Library version 2
    /// </summary>
    libpng_2_0,
    /// <summary>
    /// libtiff License
    /// </summary>
    libtiff,
    /// <summary>
    /// Licence Libre du Québec – Permissive version 1.1
    /// </summary>
    LiLiQ_P_1_1,
    /// <summary>
    /// Licence Libre du Québec – Réciprocité version 1.1
    /// </summary>
    LiLiQ_R_1_1,
    /// <summary>
    /// Licence Libre du Québec – Réciprocité forte version 1.1
    /// </summary>
    LiLiQ_Rplus_1_1,
    /// <summary>
    /// Linux Kernel Variant of OpenIB.org license
    /// </summary>
    Linux_OpenIB,
    /// <summary>
    /// Lucent Public License Version 1.0
    /// </summary>
    LPL_1_0,
    /// <summary>
    /// Lucent Public License v1.02
    /// </summary>
    LPL_1_02,
    /// <summary>
    /// LaTeX Project Public License v1.0
    /// </summary>
    LPPL_1_0,
    /// <summary>
    /// LaTeX Project Public License v1.1
    /// </summary>
    LPPL_1_1,
    /// <summary>
    /// LaTeX Project Public License v1.2
    /// </summary>
    LPPL_1_2,
    /// <summary>
    /// LaTeX Project Public License v1.3a
    /// </summary>
    LPPL_1_3a,
    /// <summary>
    /// LaTeX Project Public License v1.3c
    /// </summary>
    LPPL_1_3c,
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
    MIT_0,
    /// <summary>
    /// Enlightenment License (e16)
    /// </summary>
    MIT_advertising,
    /// <summary>
    /// CMU License
    /// </summary>
    MIT_CMU,
    /// <summary>
    /// enna License
    /// </summary>
    MIT_enna,
    /// <summary>
    /// feh License
    /// </summary>
    MIT_feh,
    /// <summary>
    /// MIT +no-false-attribs license
    /// </summary>
    MITNFA,
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
    MPL_1_0,
    /// <summary>
    /// Mozilla Public License 1.1
    /// </summary>
    MPL_1_1,
    /// <summary>
    /// Mozilla Public License 2.0
    /// </summary>
    MPL_2_0,
    /// <summary>
    /// Mozilla Public License 2.0 (no copyleft exception)
    /// </summary>
    MPL_2_0_no_copyleft_exception,
    /// <summary>
    /// Microsoft Public License
    /// </summary>
    MS_PL,
    /// <summary>
    /// Microsoft Reciprocal License
    /// </summary>
    MS_RL,
    /// <summary>
    /// Matrix Template Library License
    /// </summary>
    MTLL,
    /// <summary>
    /// Mulan Permissive Software License, Version 1
    /// </summary>
    MulanPSL_1_0,
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
    NASA_1_3,
    /// <summary>
    /// Naumen Public License
    /// </summary>
    Naumen,
    /// <summary>
    /// Net Boolean Public License v1
    /// </summary>
    NBPL_1_0,
    /// <summary>
    /// University of Illinois/NCSA Open Source License
    /// </summary>
    NCSA,
    /// <summary>
    /// Net-SNMP License
    /// </summary>
    Net_SNMP,
    /// <summary>
    /// NetCDF license
    /// </summary>
    NetCDF,
    /// <summary>
    /// Newsletr License
    /// </summary>
    Newsletr,
    /// <summary>
    /// Nethack General Public License
    /// </summary>
    NGPL,
    /// <summary>
    /// Norwegian Licence for Open Government Data
    /// </summary>
    NLOD_1_0,
    /// <summary>
    /// No Limit Public License
    /// </summary>
    NLPL,
    /// <summary>
    /// Nokia Open Source License
    /// </summary>
    Nokia,
    /// <summary>
    /// Netizen Open Source License
    /// </summary>
    NOSL,
    /// <summary>
    /// Noweb License
    /// </summary>
    Noweb,
    /// <summary>
    /// Netscape Public License v1.0
    /// </summary>
    NPL_1_0,
    /// <summary>
    /// Netscape Public License v1.1
    /// </summary>
    NPL_1_1,
    /// <summary>
    /// Non-Profit Open Software License 3.0
    /// </summary>
    NPOSL_3_0,
    /// <summary>
    /// NRL License
    /// </summary>
    NRL,
    /// <summary>
    /// NTP License
    /// </summary>
    NTP,
    /// <summary>
    /// Open CASCADE Technology Public License
    /// </summary>
    OCCT_PL,
    /// <summary>
    /// OCLC Research Public License 2.0
    /// </summary>
    OCLC_2_0,
    /// <summary>
    /// ODC Open Database License v1.0
    /// </summary>
    ODbL_1_0,
    /// <summary>
    /// Open Data Commons Attribution License v1.0
    /// </summary>
    ODC_By_1_0,
    /// <summary>
    /// SIL Open Font License 1.0
    /// </summary>
    OFL_1_0,
    /// <summary>
    /// SIL Open Font License 1.1
    /// </summary>
    OFL_1_1,
    /// <summary>
    /// Open Government Licence - Canada
    /// </summary>
    OGL_Canada_2_0,
    /// <summary>
    /// Open Government Licence v1.0
    /// </summary>
    OGL_UK_1_0,
    /// <summary>
    /// Open Government Licence v2.0
    /// </summary>
    OGL_UK_2_0,
    /// <summary>
    /// Open Government Licence v3.0
    /// </summary>
    OGL_UK_3_0,
    /// <summary>
    /// Open Group Test Suite License
    /// </summary>
    OGTSL,
    /// <summary>
    /// Open LDAP Public License v1.1
    /// </summary>
    OLDAP_1_1,
    /// <summary>
    /// Open LDAP Public License v1.2
    /// </summary>
    OLDAP_1_2,
    /// <summary>
    /// Open LDAP Public License v1.3
    /// </summary>
    OLDAP_1_3,
    /// <summary>
    /// Open LDAP Public License v1.4
    /// </summary>
    OLDAP_1_4,
    /// <summary>
    /// Open LDAP Public License v2.0 (or possibly 2.0A and 2.0B)
    /// </summary>
    OLDAP_2_0,
    /// <summary>
    /// Open LDAP Public License v2.0.1
    /// </summary>
    OLDAP_2_0_1,
    /// <summary>
    /// Open LDAP Public License v2.1
    /// </summary>
    OLDAP_2_1,
    /// <summary>
    /// Open LDAP Public License v2.2
    /// </summary>
    OLDAP_2_2,
    /// <summary>
    /// Open LDAP Public License v2.2.1
    /// </summary>
    OLDAP_2_2_1,
    /// <summary>
    /// Open LDAP Public License 2.2.2
    /// </summary>
    OLDAP_2_2_2,
    /// <summary>
    /// Open LDAP Public License v2.3
    /// </summary>
    OLDAP_2_3,
    /// <summary>
    /// Open LDAP Public License v2.4
    /// </summary>
    OLDAP_2_4,
    /// <summary>
    /// Open LDAP Public License v2.5
    /// </summary>
    OLDAP_2_5,
    /// <summary>
    /// Open LDAP Public License v2.6
    /// </summary>
    OLDAP_2_6,
    /// <summary>
    /// Open LDAP Public License v2.7
    /// </summary>
    OLDAP_2_7,
    /// <summary>
    /// Open LDAP Public License v2.8
    /// </summary>
    OLDAP_2_8,
    /// <summary>
    /// Open Market License
    /// </summary>
    OML,
    /// <summary>
    /// OpenSSL License
    /// </summary>
    OpenSSL,
    /// <summary>
    /// Open Public License v1.0
    /// </summary>
    OPL_1_0,
    /// <summary>
    /// OSET Public License version 2.1
    /// </summary>
    OSET_PL_2_1,
    /// <summary>
    /// Open Software License 1.0
    /// </summary>
    OSL_1_0,
    /// <summary>
    /// Open Software License 1.1
    /// </summary>
    OSL_1_1,
    /// <summary>
    /// Open Software License 2.0
    /// </summary>
    OSL_2_0,
    /// <summary>
    /// Open Software License 2.1
    /// </summary>
    OSL_2_1,
    /// <summary>
    /// Open Software License 3.0
    /// </summary>
    OSL_3_0,
    /// <summary>
    /// The Parity Public License 6.0.0
    /// </summary>
    Parity_6_0_0,
    /// <summary>
    /// ODC Public Domain Dedication &amp; License 1.0
    /// </summary>
    PDDL_1_0,
    /// <summary>
    /// PHP License v3.0
    /// </summary>
    PHP_3_0,
    /// <summary>
    /// PHP License v3.01
    /// </summary>
    PHP_3_01,
    /// <summary>
    /// Plexus Classworlds License
    /// </summary>
    Plexus,
    /// <summary>
    /// PostgreSQL License
    /// </summary>
    PostgreSQL,
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
    Python_2_0,
    /// <summary>
    /// Qhull License
    /// </summary>
    Qhull,
    /// <summary>
    /// Q Public License 1.0
    /// </summary>
    QPL_1_0,
    /// <summary>
    /// Rdisc License
    /// </summary>
    Rdisc,
    /// <summary>
    /// Red Hat eCos Public License v1.1
    /// </summary>
    RHeCos_1_1,
    /// <summary>
    /// Reciprocal Public License 1.1
    /// </summary>
    RPL_1_1,
    /// <summary>
    /// Reciprocal Public License 1.5
    /// </summary>
    RPL_1_5,
    /// <summary>
    /// RealNetworks Public Source License v1.0
    /// </summary>
    RPSL_1_0,
    /// <summary>
    /// RSA Message-Digest License 
    /// </summary>
    RSA_MD,
    /// <summary>
    /// Ricoh Source Code Public License
    /// </summary>
    RSCPL,
    /// <summary>
    /// Ruby License
    /// </summary>
    Ruby,
    /// <summary>
    /// Sax Public Domain Notice
    /// </summary>
    SAX_PD,
    /// <summary>
    /// Saxpath License
    /// </summary>
    Saxpath,
    /// <summary>
    /// SCEA Shared Source License
    /// </summary>
    SCEA,
    /// <summary>
    /// Sendmail License
    /// </summary>
    Sendmail,
    /// <summary>
    /// Sendmail License 8.23
    /// </summary>
    Sendmail_8_23,
    /// <summary>
    /// SGI Free Software License B v1.0
    /// </summary>
    SGI_B_1_0,
    /// <summary>
    /// SGI Free Software License B v1.1
    /// </summary>
    SGI_B_1_1,
    /// <summary>
    /// SGI Free Software License B v2.0
    /// </summary>
    SGI_B_2_0,
    /// <summary>
    /// Solderpad Hardware License v0.5
    /// </summary>
    SHL_0_5,
    /// <summary>
    /// Solderpad Hardware License, Version 0.51
    /// </summary>
    SHL_0_51,
    /// <summary>
    /// Simple Public License 2.0
    /// </summary>
    SimPL_2_0,
    /// <summary>
    /// Sun Industry Standards Source License v1.1
    /// </summary>
    SISSL,
    /// <summary>
    /// Sun Industry Standards Source License v1.2
    /// </summary>
    SISSL_1_2,
    /// <summary>
    /// Sleepycat License
    /// </summary>
    Sleepycat,
    /// <summary>
    /// Standard ML of New Jersey License
    /// </summary>
    SMLNJ,
    /// <summary>
    /// Secure Messaging Protocol Public License
    /// </summary>
    SMPPL,
    /// <summary>
    /// SNIA Public License 1.1
    /// </summary>
    SNIA,
    /// <summary>
    /// Spencer License 86
    /// </summary>
    Spencer_86,
    /// <summary>
    /// Spencer License 94
    /// </summary>
    Spencer_94,
    /// <summary>
    /// Spencer License 99
    /// </summary>
    Spencer_99,
    /// <summary>
    /// Sun Public License v1.0
    /// </summary>
    SPL_1_0,
    /// <summary>
    /// SSH OpenSSH license
    /// </summary>
    SSH_OpenSSH,
    /// <summary>
    /// SSH short notice
    /// </summary>
    SSH_short,
    /// <summary>
    /// Server Side Public License, v 1
    /// </summary>
    SSPL_1_0,
    /// <summary>
    /// SugarCRM Public License v1.1.3
    /// </summary>
    SugarCRM_1_1_3,
    /// <summary>
    /// Scheme Widget Library (SWL) Software License Agreement
    /// </summary>
    SWL,
    /// <summary>
    /// TAPR Open Hardware License v1.0
    /// </summary>
    TAPR_OHL_1_0,
    /// <summary>
    /// TCL/TK License
    /// </summary>
    TCL,
    /// <summary>
    /// TCP Wrappers License
    /// </summary>
    TCP_wrappers,
    /// <summary>
    /// TMate Open Source License
    /// </summary>
    TMate,
    /// <summary>
    /// TORQUE v2.5+ Software License v1.1
    /// </summary>
    TORQUE_1_1,
    /// <summary>
    /// Trusster Open Source License
    /// </summary>
    TOSL,
    /// <summary>
    /// Technische Universitaet Berlin License 1.0
    /// </summary>
    TU_Berlin_1_0,
    /// <summary>
    /// Technische Universitaet Berlin License 2.0
    /// </summary>
    TU_Berlin_2_0,
    /// <summary>
    /// Upstream Compatibility License v1.0
    /// </summary>
    UCL_1_0,
    /// <summary>
    /// Unicode License Agreement - Data Files and Software (2015)
    /// </summary>
    Unicode_DFS_2015,
    /// <summary>
    /// Unicode License Agreement - Data Files and Software (2016)
    /// </summary>
    Unicode_DFS_2016,
    /// <summary>
    /// Unicode Terms of Use
    /// </summary>
    Unicode_TOU,
    /// <summary>
    /// The Unlicense
    /// </summary>
    Unlicense,
    /// <summary>
    /// Universal Permissive License v1.0
    /// </summary>
    UPL_1_0,
    /// <summary>
    /// Vim License
    /// </summary>
    Vim,
    /// <summary>
    /// VOSTROM Public License for Open Source
    /// </summary>
    VOSTROM,
    /// <summary>
    /// Vovida Software License v1.0
    /// </summary>
    VSL_1_0,
    /// <summary>
    /// W3C Software Notice and License (2002-12-31)
    /// </summary>
    W3C,
    /// <summary>
    /// W3C Software Notice and License (1998-07-20)
    /// </summary>
    W3C_19980720,
    /// <summary>
    /// W3C Software Notice and Document License (2015-05-13)
    /// </summary>
    W3C_20150513,
    /// <summary>
    /// Sybase Open Watcom Public License 1.0
    /// </summary>
    Watcom_1_0,
    /// <summary>
    /// Wsuipa License
    /// </summary>
    Wsuipa,
    /// <summary>
    /// Do What The F*ck You Want To Public License
    /// </summary>
    WTFPL,
    /// <summary>
    /// X11 License
    /// </summary>
    X11,
    /// <summary>
    /// Xerox License
    /// </summary>
    Xerox,
    /// <summary>
    /// XFree86 License 1.1
    /// </summary>
    XFree86_1_1,
    /// <summary>
    /// xinetd License
    /// </summary>
    xinetd,
    /// <summary>
    /// X.Net License
    /// </summary>
    Xnet,
    /// <summary>
    /// XPP License
    /// </summary>
    xpp,
    /// <summary>
    /// XSkat License
    /// </summary>
    XSkat,
    /// <summary>
    /// Yahoo! Public License v1.0
    /// </summary>
    YPL_1_0,
    /// <summary>
    /// Yahoo! Public License v1.1
    /// </summary>
    YPL_1_1,
    /// <summary>
    /// Zed License
    /// </summary>
    Zed,
    /// <summary>
    /// Zend License v2.0
    /// </summary>
    Zend_2_0,
    /// <summary>
    /// Zimbra Public License v1.3
    /// </summary>
    Zimbra_1_3,
    /// <summary>
    /// Zimbra Public License v1.4
    /// </summary>
    Zimbra_1_4,
    /// <summary>
    /// zlib License
    /// </summary>
    Zlib,
    /// <summary>
    /// zlib/libpng License with Acknowledgement
    /// </summary>
    zlib_acknowledgement,
    /// <summary>
    /// Zope Public License 1.1
    /// </summary>
    ZPL_1_1,
    /// <summary>
    /// Zope Public License 2.0
    /// </summary>
    ZPL_2_0,
    /// <summary>
    /// Zope Public License 2.1
    /// </summary>
    ZPL_2_1,
}
