import React from 'react'
import PropTypes from 'prop-types'
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { Carousel } from 'react-responsive-carousel';
import CarouselSlide from './CarouselSlide'


function SimpleCarousel(props) {
  return (
    <Carousel infiniteLoop useKeyboardArrows autoPlay>
      {props.images.map((slide, index) => {
        return (
          <CarouselSlide key={index} src={slide.src} alt={slide.alt} />
        )
      })}
    </Carousel>
  )
}

SimpleCarousel.propTypes = {
  images: PropTypes.arrayOf(PropTypes.object).isRequired,
}

export default SimpleCarousel