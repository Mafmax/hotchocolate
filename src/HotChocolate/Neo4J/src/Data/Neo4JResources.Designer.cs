﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotChocolate.Data.Neo4J {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Neo4JResources {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Neo4JResources() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("HotChocolate.Data.Neo4J.Neo4JResources", typeof(Neo4JResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string ErrorHelper_Filtering_CreateNonNullError {
            get {
                return ResourceManager.GetString("ErrorHelper_Filtering_CreateNonNullError", resourceCulture);
            }
        }
        
        internal static string ErrorHelper_Sorting_CreateNonNullError {
            get {
                return ResourceManager.GetString("ErrorHelper_Sorting_CreateNonNullError", resourceCulture);
            }
        }
        
        internal static string Paging_SourceIsNotSupported {
            get {
                return ResourceManager.GetString("Paging_SourceIsNotSupported", resourceCulture);
            }
        }
        
        internal static string ValueMapper_CypherValueIsNotAListAndCannotBeMapped {
            get {
                return ResourceManager.GetString("ValueMapper_CypherValueIsNotAListAndCannotBeMapped", resourceCulture);
            }
        }
        
        internal static string ValueMapper_CypherValueIsAListAndCannotBeMapped {
            get {
                return ResourceManager.GetString("ValueMapper_CypherValueIsAListAndCannotBeMapped", resourceCulture);
            }
        }
        
        internal static string Language_UnsupportedLiteralType {
            get {
                return ResourceManager.GetString("Language_UnsupportedLiteralType", resourceCulture);
            }
        }
        
        internal static string Language_NeedsAParentWithSymbolicName {
            get {
                return ResourceManager.GetString("Language_NeedsAParentWithSymbolicName", resourceCulture);
            }
        }
        
        internal static string Projection_RelationshipDirectionNotSet {
            get {
                return ResourceManager.GetString("Projection_RelationshipDirectionNotSet", resourceCulture);
            }
        }
        
        internal static string Filtering_Neo4JCombinator_QueueEmpty {
            get {
                return ResourceManager.GetString("Filtering_Neo4JCombinator_QueueEmpty", resourceCulture);
            }
        }
        
        internal static string Filtering_Neo4JCombinator_InvalidCombinator {
            get {
                return ResourceManager.GetString("Filtering_Neo4JCombinator_InvalidCombinator", resourceCulture);
            }
        }
    }
}
