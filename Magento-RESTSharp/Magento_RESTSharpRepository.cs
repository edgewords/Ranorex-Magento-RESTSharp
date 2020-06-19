﻿///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// DO NOT MODIFY THIS FILE! It is regenerated by the designer.
// All your modifications will be lost!
// http://www.ranorex.com
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace Magento_RESTSharp
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    /// The class representing the Magento_RESTSharpRepository element repository.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", global::Ranorex.Core.Constants.CodeGenVersion)]
    [RepositoryFolder("31f6b1c3-bd2e-4174-b0e3-adcf52eecea1")]
    public partial class Magento_RESTSharpRepository : RepoGenBaseFolder
    {
        static Magento_RESTSharpRepository instance = new Magento_RESTSharpRepository();

        /// <summary>
        /// Gets the singleton class instance representing the Magento_RESTSharpRepository element repository.
        /// </summary>
        [RepositoryFolder("31f6b1c3-bd2e-4174-b0e3-adcf52eecea1")]
        public static Magento_RESTSharpRepository Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Repository class constructor.
        /// </summary>
        public Magento_RESTSharpRepository() 
            : base("Magento_RESTSharpRepository", "/", null, 0, false, "31f6b1c3-bd2e-4174-b0e3-adcf52eecea1", ".\\RepositoryImages\\Magento_RESTSharpRepository31f6b1c3.rximgres")
        {
        }

#region Variables

#endregion

        /// <summary>
        /// The Self item info.
        /// </summary>
        [RepositoryItemInfo("31f6b1c3-bd2e-4174-b0e3-adcf52eecea1")]
        public virtual RepoItemInfo SelfInfo
        {
            get
            {
                return _selfInfo;
            }
        }
    }

    /// <summary>
    /// Inner folder classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", global::Ranorex.Core.Constants.CodeGenVersion)]
    public partial class Magento_RESTSharpRepositoryFolders
    {
    }
#pragma warning restore 0436
}
