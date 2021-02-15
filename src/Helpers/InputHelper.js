import React, { useState } from 'react'

export function useInput(defaultValue = ''){
    const [value, setValue] = useState(defaultValue)

    return{
        bind: {
            value: value,
            onChange: event => setValue(event.target.value)
        },
        clear:() => setValue(''),
        value: () => value,
        setValue
    }
}