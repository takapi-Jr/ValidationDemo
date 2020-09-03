using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ValidationDemo.Models;

namespace ValidationDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        [Required(ErrorMessage = "必須項目")]
        public ReactiveProperty<string> InputText1 { get; }

        [Required(ErrorMessage = "必須項目")]
        [IntValidation(ErrorMessage = "整数を入力してください")]
        [Range(-100, 100, ErrorMessage = "範囲内の値を入力してください")]
        public ReactiveProperty<string> InputText2 { get; }
        
        [Required(ErrorMessage = "必須項目")]
        [DoubleValidation(ErrorMessage = "実数を入力してください")]
        [Range(-100.0, 100.0, ErrorMessage = "範囲内の値を入力してください")]
        public ReactiveProperty<string> InputText3 { get; }

        public ReadOnlyReactiveProperty<string> ErrorText1 { get; }
        public ReadOnlyReactiveProperty<string> ErrorText2 { get; }
        public ReadOnlyReactiveProperty<string> ErrorText3 { get; }

        public ReactiveCommand ExecCommand { get; }



        public MainWindowViewModel()
        {
            // バリデーション属性設定
            this.InputText1 = new ReactiveProperty<string>().SetValidateAttribute(() => this.InputText1);
            this.InputText2 = new ReactiveProperty<string>().SetValidateAttribute(() => this.InputText2);
            this.InputText3 = new ReactiveProperty<string>().SetValidateAttribute(() => this.InputText3);

            // バリデーションエラー表示設定
            this.ErrorText1 = this.InputText1.ObserveErrorChanged
                .Select(x => x?.Cast<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();
            this.ErrorText2 = this.InputText2.ObserveErrorChanged
                .Select(x => x?.Cast<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();
            this.ErrorText3 = this.InputText3.ObserveErrorChanged
                .Select(x => x?.Cast<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();

            // 実行コマンドのアクティブ設定
            this.ExecCommand =
                new[]
                {
                    this.InputText1.ObserveHasErrors,
                    this.InputText2.ObserveHasErrors,
                    this.InputText3.ObserveHasErrors,
                }
                .CombineLatestValuesAreAllFalse()   // すべてエラーなしの場合にアクティブ設定
                .ToReactiveCommand();
        }
    }
}
