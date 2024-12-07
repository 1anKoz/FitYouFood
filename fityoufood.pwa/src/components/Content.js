import React, { useEffect, useState } from 'react';
import { fetchContent } from '../services/api';

const Content = () => {
    const [content, setContent] = useState([]);

    useEffect(() => {
        const loadContent = async () => {
            const response = await fetchContent();
            setContent(response.data);
        };
        loadContent();
    }, []);

    return (
        <div>
            {content.map((item, index) => (
                <div key={index}>
                    <h3>{item.title}</h3>
                    <img src={item.image} alt={item.title} />
                    <p>{item.text}</p>
                </div>
            ))}
        </div>
    );
};

export default Content;
