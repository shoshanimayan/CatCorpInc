using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Threading;
using System.Threading.Tasks;

namespace Ui
{
	public class TextDispalyView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private int _textDelay=50;
        ///  PRIVATE VARIABLES         ///
        private bool _writing;
        private CancellationTokenSource _cToken;
        private TextDispalyMediator _mediator;
        ///  PRIVATE METHODS           ///
        private async Task TypeText( CancellationToken t = default)
        {

            _writing = true;
            _textField.maxVisibleCharacters = 0;
            var TotalVisibleCharacters = _textField.textInfo.pageInfo[_textField.pageToDisplay - 1].lastCharacterIndex - _textField.textInfo.pageInfo[_textField.pageToDisplay - 1].firstCharacterIndex;
            int counter = _textField.textInfo.pageInfo[_textField.pageToDisplay - 1].firstCharacterIndex;
            _mediator.PlayTextAudio();

            while (counter <= _textField.textInfo.pageInfo[_textField.pageToDisplay - 1].lastCharacterIndex+1)
            {
                if (t.IsCancellationRequested)
                {
                    _textField.maxVisibleCharacters = _textField.textInfo.pageInfo[_textField.pageToDisplay - 1].lastCharacterIndex+1;
                    _writing = false;

                    return;
                }
                int visibleCount = counter ;

                _textField.maxVisibleCharacters = visibleCount;


                counter++;
                await Task.Delay(_textDelay);
            }
            _writing = false;
        }


        ///  PUBLIC API                ///
        public void SetName(string name)
        { 
            _name.text = name;
        }

        public async void SetText(string text)
        {
            if (_cToken == null)
            {
                _cToken = new CancellationTokenSource();

            }
            else {
                _cToken.Cancel();
                _cToken.Dispose();
                _cToken = new CancellationTokenSource();
            }
            _textField.text = text;
            _textField.pageToDisplay = 1;
            _textField.ForceMeshUpdate();
            await TypeText(_cToken.Token);

        }

        public int GetTotalPages()
        {

            return _textField.textInfo.pageCount;
        }

        public int GetCurrentPage()
        {
            return _textField.pageToDisplay;
        }

        public void Initilization(TextDispalyMediator mediator)
        {
            _mediator = mediator;
        }

        public void ClearToken()
        {
            _cToken.Cancel();
            _cToken.Dispose();
            _cToken = null;
        }

        public async void IncrementPage() {

            if ( _writing && _cToken != null)
            {
                _cToken.Cancel();
                _cToken.Dispose();
                _cToken = new CancellationTokenSource();
                return;
            }


            _textField.pageToDisplay++;
            if (_textField.textInfo.pageCount < _textField.pageToDisplay)
            {
                _mediator.ForceEnd();
                return;
            }
             await TypeText(_cToken.Token);

        }






    }
}
