import React from "react";

 function WordStatistic (props)  
 {
     return (
        <div>
            <span>Total count: {props.count}</span>
            <ul>
            {props.occurrences.map((node, index) => {
                return (                
                    <li key={index}>{node.key}: {node.count}</li>
                )
            })}
        </ul>
        </div>
     )
}

export default WordStatistic