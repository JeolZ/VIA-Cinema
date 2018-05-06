﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace VIA_Cinema.localhost {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="VIACinemaServiceSoap", Namespace="http://localhost/")]
    public partial class VIACinemaService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetAllMoviesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMoviesOfDayOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetMovieInfoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public VIACinemaService() {
            this.Url = global::VIA_Cinema.Properties.Settings.Default.VIA_Cinema_localhost_VIACinemaService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetAllMoviesCompletedEventHandler GetAllMoviesCompleted;
        
        /// <remarks/>
        public event GetMoviesOfDayCompletedEventHandler GetMoviesOfDayCompleted;
        
        /// <remarks/>
        public event GetMovieInfoCompletedEventHandler GetMovieInfoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/GetAllMovies", RequestNamespace="http://localhost/", ResponseNamespace="http://localhost/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Movie[] GetAllMovies() {
            object[] results = this.Invoke("GetAllMovies", new object[0]);
            return ((Movie[])(results[0]));
        }
        
        /// <remarks/>
        public void GetAllMoviesAsync() {
            this.GetAllMoviesAsync(null);
        }
        
        /// <remarks/>
        public void GetAllMoviesAsync(object userState) {
            if ((this.GetAllMoviesOperationCompleted == null)) {
                this.GetAllMoviesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllMoviesOperationCompleted);
            }
            this.InvokeAsync("GetAllMovies", new object[0], this.GetAllMoviesOperationCompleted, userState);
        }
        
        private void OnGetAllMoviesOperationCompleted(object arg) {
            if ((this.GetAllMoviesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllMoviesCompleted(this, new GetAllMoviesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/GetMoviesOfDay", RequestNamespace="http://localhost/", ResponseNamespace="http://localhost/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Movie[] GetMoviesOfDay(int day) {
            object[] results = this.Invoke("GetMoviesOfDay", new object[] {
                        day});
            return ((Movie[])(results[0]));
        }
        
        /// <remarks/>
        public void GetMoviesOfDayAsync(int day) {
            this.GetMoviesOfDayAsync(day, null);
        }
        
        /// <remarks/>
        public void GetMoviesOfDayAsync(int day, object userState) {
            if ((this.GetMoviesOfDayOperationCompleted == null)) {
                this.GetMoviesOfDayOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMoviesOfDayOperationCompleted);
            }
            this.InvokeAsync("GetMoviesOfDay", new object[] {
                        day}, this.GetMoviesOfDayOperationCompleted, userState);
        }
        
        private void OnGetMoviesOfDayOperationCompleted(object arg) {
            if ((this.GetMoviesOfDayCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMoviesOfDayCompleted(this, new GetMoviesOfDayCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/GetMovieInfo", RequestNamespace="http://localhost/", ResponseNamespace="http://localhost/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Movie GetMovieInfo(int id) {
            object[] results = this.Invoke("GetMovieInfo", new object[] {
                        id});
            return ((Movie)(results[0]));
        }
        
        /// <remarks/>
        public void GetMovieInfoAsync(int id) {
            this.GetMovieInfoAsync(id, null);
        }
        
        /// <remarks/>
        public void GetMovieInfoAsync(int id, object userState) {
            if ((this.GetMovieInfoOperationCompleted == null)) {
                this.GetMovieInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMovieInfoOperationCompleted);
            }
            this.InvokeAsync("GetMovieInfo", new object[] {
                        id}, this.GetMovieInfoOperationCompleted, userState);
        }
        
        private void OnGetMovieInfoOperationCompleted(object arg) {
            if ((this.GetMovieInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMovieInfoCompleted(this, new GetMovieInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/")]
    public partial class Movie {
        
        private int idField;
        
        private string titleField;
        
        private string descriptionField;
        
        private int durationField;
        
        private string releaseDateField;
        
        private Show[] showsField;
        
        /// <remarks/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        public int Duration {
            get {
                return this.durationField;
            }
            set {
                this.durationField = value;
            }
        }
        
        /// <remarks/>
        public string ReleaseDate {
            get {
                return this.releaseDateField;
            }
            set {
                this.releaseDateField = value;
            }
        }
        
        /// <remarks/>
        public Show[] Shows {
            get {
                return this.showsField;
            }
            set {
                this.showsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/")]
    public partial class Show {
        
        private int idField;
        
        private System.DateTime dateField;
        
        private int roomField;
        
        /// <remarks/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime Date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
            }
        }
        
        /// <remarks/>
        public int Room {
            get {
                return this.roomField;
            }
            set {
                this.roomField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void GetAllMoviesCompletedEventHandler(object sender, GetAllMoviesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllMoviesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllMoviesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Movie[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Movie[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void GetMoviesOfDayCompletedEventHandler(object sender, GetMoviesOfDayCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMoviesOfDayCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMoviesOfDayCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Movie[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Movie[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void GetMovieInfoCompletedEventHandler(object sender, GetMovieInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMovieInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetMovieInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Movie Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Movie)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591