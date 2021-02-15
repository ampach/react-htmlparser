import React, { useCallback, useState } from 'react'
import Loader from './Loader'
import SimpleCarousel from './Carousel/SimpleCarousel'
import {useInput} from './Helpers/InputHelper'
import validator from 'validator'
import config from './config/default.json'
import WordStatistic from "./Statistic/wordStatistic";

function useInputValue(defaultValue = ''){
  const [value, setValue] = useState(defaultValue)

  return{
      bind: {
          value: value,
          onChange: event => setValue(event.target.value)
      },
      clear:() => setValue(''),
      value: () => value
  }
}

function App() {
  const [loading, setLoading] = React.useState(false)
  const [images, setImages] = React.useState([])
  const [occurrences, setOccurrences] = React.useState([])
  const [count, setCount] = React.useState(0)
  
  const [message, setMessage] = React.useState('')
  const { value, bind, clear:ClearValue } = useInput('');

 
  const ParseURL = useCallback((url) => {
    let urlEncoded = encodeURIComponent(url)
    fetch(`${config.serverHost}?url=${urlEncoded}`)
    .then(response => response.json())
    .then(result => {
      setImages(result.imagaes)
      setOccurrences(result.occurrences)
      setCount(result.numberOfWords)
      setLoading(false)
    })
  }, [])

  const ClearPage = () => {
    setImages([])
    setOccurrences([])
    setCount(0)
    setMessage("")    
  }

  const onClearClick = () => {
    ClearValue()
    ClearPage()
  }

 
  const onParseClick = () => {
    if(validator.isURL(value())){
      setLoading(true)
        ParseURL(value());
      }else{
        setMessage("URL is wrong")
      }    
  }
  

  return (
    <div className='wrapper wrapper-content'>
      <div className="container">
        <h1>Parse Any HTML here</h1>
        <div className='row'>
          <div className="col">
            <form>
              <div className="form-group">
                <label htmlFor="url">URL:</label>
                <input name="url" className="form-control" type="text"  {...bind} />
                {message && 
                  <span style={{ fontWeight: 'bold', color: 'red' }}>{message}</span>
                }
              </div>
              <div className="btn-toolbar float-right" role="toolbar">
                <div className="btn-group mr-2" role="group" aria-label="First group">
                  <button type="button" className="btn btn-success" onClick={onParseClick}>Parse</button>
                </div>
                <div className="btn-group mr-2" role="group" aria-label="First group">
                  <button type="button" className="btn btn-primary" onClick={ onClearClick }>Clear</button>
                </div>
              </div>
              
            </form>

          </div>
        </div>
        <div className='row pt-4'>
          <div className="col">
            {loading && <Loader />}
            {images.length > 0 && <SimpleCarousel images={images} /> }
            {count > 0 && <WordStatistic count={count} occurrences={occurrences} /> }
          </div>
        </div>
        
        
        </div>
      </div>
  )
}

export default App
