import React from 'react'
import PropTypes from 'prop-types'

function CarouselSlide(props) {
  return (
    <div>
        <img src={props.src} alt={props.alt} />
    </div> 
  )
}

CarouselSlide.propTypes = {
  src: PropTypes.string.isRequired,
  alt: PropTypes.string
}

export default CarouselSlide
