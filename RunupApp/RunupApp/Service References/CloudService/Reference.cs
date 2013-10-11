﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.Phone.ServiceReference, version 3.7.0.0
// 
namespace RunupApp.CloudService {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Users", Namespace="http://schemas.datacontract.org/2004/07/CloudService.Database", IsReference=true)]
    public partial class Users : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string EmailField;
        
        private string PasswordField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CloudService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService/Login", ReplyAction="http://tempuri.org/IService/LoginResponse")]
        System.IAsyncResult BeginLogin(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState);
        
        bool EndLogin(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService/Register", ReplyAction="http://tempuri.org/IService/RegisterResponse")]
        System.IAsyncResult BeginRegister(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState);
        
        bool EndRegister(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService/SaveData", ReplyAction="http://tempuri.org/IService/SaveDataResponse")]
        System.IAsyncResult BeginSaveData(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState);
        
        bool EndSaveData(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IService/LoadData", ReplyAction="http://tempuri.org/IService/LoadDataResponse")]
        System.IAsyncResult BeginLoadData(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState);
        
        bool EndLoadData(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : RunupApp.CloudService.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RegisterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public RegisterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SaveDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public SaveDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoadDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public LoadDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<RunupApp.CloudService.IService>, RunupApp.CloudService.IService {
        
        private BeginOperationDelegate onBeginLoginDelegate;
        
        private EndOperationDelegate onEndLoginDelegate;
        
        private System.Threading.SendOrPostCallback onLoginCompletedDelegate;
        
        private BeginOperationDelegate onBeginRegisterDelegate;
        
        private EndOperationDelegate onEndRegisterDelegate;
        
        private System.Threading.SendOrPostCallback onRegisterCompletedDelegate;
        
        private BeginOperationDelegate onBeginSaveDataDelegate;
        
        private EndOperationDelegate onEndSaveDataDelegate;
        
        private System.Threading.SendOrPostCallback onSaveDataCompletedDelegate;
        
        private BeginOperationDelegate onBeginLoadDataDelegate;
        
        private EndOperationDelegate onEndLoadDataDelegate;
        
        private System.Threading.SendOrPostCallback onLoadDataCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<LoginCompletedEventArgs> LoginCompleted;
        
        public event System.EventHandler<RegisterCompletedEventArgs> RegisterCompleted;
        
        public event System.EventHandler<SaveDataCompletedEventArgs> SaveDataCompleted;
        
        public event System.EventHandler<LoadDataCompletedEventArgs> LoadDataCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult RunupApp.CloudService.IService.BeginLogin(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogin(user, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bool RunupApp.CloudService.IService.EndLogin(System.IAsyncResult result) {
            return base.Channel.EndLogin(result);
        }
        
        private System.IAsyncResult OnBeginLogin(object[] inValues, System.AsyncCallback callback, object asyncState) {
            RunupApp.CloudService.Users user = ((RunupApp.CloudService.Users)(inValues[0]));
            return ((RunupApp.CloudService.IService)(this)).BeginLogin(user, callback, asyncState);
        }
        
        private object[] OnEndLogin(System.IAsyncResult result) {
            bool retVal = ((RunupApp.CloudService.IService)(this)).EndLogin(result);
            return new object[] {
                    retVal};
        }
        
        private void OnLoginCompleted(object state) {
            if ((this.LoginCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LoginCompleted(this, new LoginCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LoginAsync(RunupApp.CloudService.Users user) {
            this.LoginAsync(user, null);
        }
        
        public void LoginAsync(RunupApp.CloudService.Users user, object userState) {
            if ((this.onBeginLoginDelegate == null)) {
                this.onBeginLoginDelegate = new BeginOperationDelegate(this.OnBeginLogin);
            }
            if ((this.onEndLoginDelegate == null)) {
                this.onEndLoginDelegate = new EndOperationDelegate(this.OnEndLogin);
            }
            if ((this.onLoginCompletedDelegate == null)) {
                this.onLoginCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLoginCompleted);
            }
            base.InvokeAsync(this.onBeginLoginDelegate, new object[] {
                        user}, this.onEndLoginDelegate, this.onLoginCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult RunupApp.CloudService.IService.BeginRegister(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginRegister(user, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bool RunupApp.CloudService.IService.EndRegister(System.IAsyncResult result) {
            return base.Channel.EndRegister(result);
        }
        
        private System.IAsyncResult OnBeginRegister(object[] inValues, System.AsyncCallback callback, object asyncState) {
            RunupApp.CloudService.Users user = ((RunupApp.CloudService.Users)(inValues[0]));
            return ((RunupApp.CloudService.IService)(this)).BeginRegister(user, callback, asyncState);
        }
        
        private object[] OnEndRegister(System.IAsyncResult result) {
            bool retVal = ((RunupApp.CloudService.IService)(this)).EndRegister(result);
            return new object[] {
                    retVal};
        }
        
        private void OnRegisterCompleted(object state) {
            if ((this.RegisterCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.RegisterCompleted(this, new RegisterCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void RegisterAsync(RunupApp.CloudService.Users user) {
            this.RegisterAsync(user, null);
        }
        
        public void RegisterAsync(RunupApp.CloudService.Users user, object userState) {
            if ((this.onBeginRegisterDelegate == null)) {
                this.onBeginRegisterDelegate = new BeginOperationDelegate(this.OnBeginRegister);
            }
            if ((this.onEndRegisterDelegate == null)) {
                this.onEndRegisterDelegate = new EndOperationDelegate(this.OnEndRegister);
            }
            if ((this.onRegisterCompletedDelegate == null)) {
                this.onRegisterCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnRegisterCompleted);
            }
            base.InvokeAsync(this.onBeginRegisterDelegate, new object[] {
                        user}, this.onEndRegisterDelegate, this.onRegisterCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult RunupApp.CloudService.IService.BeginSaveData(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSaveData(user, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bool RunupApp.CloudService.IService.EndSaveData(System.IAsyncResult result) {
            return base.Channel.EndSaveData(result);
        }
        
        private System.IAsyncResult OnBeginSaveData(object[] inValues, System.AsyncCallback callback, object asyncState) {
            RunupApp.CloudService.Users user = ((RunupApp.CloudService.Users)(inValues[0]));
            return ((RunupApp.CloudService.IService)(this)).BeginSaveData(user, callback, asyncState);
        }
        
        private object[] OnEndSaveData(System.IAsyncResult result) {
            bool retVal = ((RunupApp.CloudService.IService)(this)).EndSaveData(result);
            return new object[] {
                    retVal};
        }
        
        private void OnSaveDataCompleted(object state) {
            if ((this.SaveDataCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SaveDataCompleted(this, new SaveDataCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SaveDataAsync(RunupApp.CloudService.Users user) {
            this.SaveDataAsync(user, null);
        }
        
        public void SaveDataAsync(RunupApp.CloudService.Users user, object userState) {
            if ((this.onBeginSaveDataDelegate == null)) {
                this.onBeginSaveDataDelegate = new BeginOperationDelegate(this.OnBeginSaveData);
            }
            if ((this.onEndSaveDataDelegate == null)) {
                this.onEndSaveDataDelegate = new EndOperationDelegate(this.OnEndSaveData);
            }
            if ((this.onSaveDataCompletedDelegate == null)) {
                this.onSaveDataCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSaveDataCompleted);
            }
            base.InvokeAsync(this.onBeginSaveDataDelegate, new object[] {
                        user}, this.onEndSaveDataDelegate, this.onSaveDataCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult RunupApp.CloudService.IService.BeginLoadData(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLoadData(user, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        bool RunupApp.CloudService.IService.EndLoadData(System.IAsyncResult result) {
            return base.Channel.EndLoadData(result);
        }
        
        private System.IAsyncResult OnBeginLoadData(object[] inValues, System.AsyncCallback callback, object asyncState) {
            RunupApp.CloudService.Users user = ((RunupApp.CloudService.Users)(inValues[0]));
            return ((RunupApp.CloudService.IService)(this)).BeginLoadData(user, callback, asyncState);
        }
        
        private object[] OnEndLoadData(System.IAsyncResult result) {
            bool retVal = ((RunupApp.CloudService.IService)(this)).EndLoadData(result);
            return new object[] {
                    retVal};
        }
        
        private void OnLoadDataCompleted(object state) {
            if ((this.LoadDataCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LoadDataCompleted(this, new LoadDataCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LoadDataAsync(RunupApp.CloudService.Users user) {
            this.LoadDataAsync(user, null);
        }
        
        public void LoadDataAsync(RunupApp.CloudService.Users user, object userState) {
            if ((this.onBeginLoadDataDelegate == null)) {
                this.onBeginLoadDataDelegate = new BeginOperationDelegate(this.OnBeginLoadData);
            }
            if ((this.onEndLoadDataDelegate == null)) {
                this.onEndLoadDataDelegate = new EndOperationDelegate(this.OnEndLoadData);
            }
            if ((this.onLoadDataCompletedDelegate == null)) {
                this.onLoadDataCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLoadDataCompleted);
            }
            base.InvokeAsync(this.onBeginLoadDataDelegate, new object[] {
                        user}, this.onEndLoadDataDelegate, this.onLoadDataCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override RunupApp.CloudService.IService CreateChannel() {
            return new ServiceClientChannel(this);
        }
        
        private class ServiceClientChannel : ChannelBase<RunupApp.CloudService.IService>, RunupApp.CloudService.IService {
            
            public ServiceClientChannel(System.ServiceModel.ClientBase<RunupApp.CloudService.IService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginLogin(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = user;
                System.IAsyncResult _result = base.BeginInvoke("Login", _args, callback, asyncState);
                return _result;
            }
            
            public bool EndLogin(System.IAsyncResult result) {
                object[] _args = new object[0];
                bool _result = ((bool)(base.EndInvoke("Login", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginRegister(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = user;
                System.IAsyncResult _result = base.BeginInvoke("Register", _args, callback, asyncState);
                return _result;
            }
            
            public bool EndRegister(System.IAsyncResult result) {
                object[] _args = new object[0];
                bool _result = ((bool)(base.EndInvoke("Register", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginSaveData(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = user;
                System.IAsyncResult _result = base.BeginInvoke("SaveData", _args, callback, asyncState);
                return _result;
            }
            
            public bool EndSaveData(System.IAsyncResult result) {
                object[] _args = new object[0];
                bool _result = ((bool)(base.EndInvoke("SaveData", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginLoadData(RunupApp.CloudService.Users user, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = user;
                System.IAsyncResult _result = base.BeginInvoke("LoadData", _args, callback, asyncState);
                return _result;
            }
            
            public bool EndLoadData(System.IAsyncResult result) {
                object[] _args = new object[0];
                bool _result = ((bool)(base.EndInvoke("LoadData", _args, result)));
                return _result;
            }
        }
    }
}