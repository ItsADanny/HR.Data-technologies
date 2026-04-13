import { useEffect, useState } from "react";
import "./Slideshow.css";

type ImageSlide = {
    src: string;
    alt: string;
};

interface SlideshowProps {
    compact?: boolean;
    imageSlides?: ImageSlide[];
}

const slides = [
    {
        id: 1,
        title: "Unleash Your Gaming Potential",
        description: "Build smoother gameplay with elite CPUs, GPUs, and memory.",
        cta: "Shop Components",
        theme: "slide-theme-1",
    },
    {
        id: 2,
        title: "Precision-Crafted Workstations",
        description: "Power through rendering, coding, and editing with pro-grade parts.",
        cta: "View Workstations",
        theme: "slide-theme-2",
    },
    {
        id: 3,
        title: "Upgrade Week Deals",
        description: "Discover limited-time savings on trusted hardware brands.",
        cta: "See Promotions",
        theme: "slide-theme-3",
    },
];

export default function Slideshow({ compact = false, imageSlides = [] }: SlideshowProps) {
    const [currentIndex, setCurrentIndex] = useState(0);
    const hasImageSlides = imageSlides.length > 0;
    const totalSlides = hasImageSlides ? imageSlides.length : slides.length;

    const nextSlide = () => {
        setCurrentIndex((prev) => (prev + 1) % totalSlides);
    };

    const prevSlide = () => {
        setCurrentIndex((prev) => (prev - 1 + totalSlides) % totalSlides);
    };

    useEffect(() => {
        const intervalId = window.setInterval(() => {
            setCurrentIndex((prev) => (prev + 1) % totalSlides);
        }, 5000);

        return () => {
            window.clearInterval(intervalId);
        };
    }, [totalSlides]);

    return (
        <section className={`slideshow-container${compact ? ' compact' : ''}`} aria-label="Featured products">
            <div className="slideshow-track" style={{ transform: `translateX(-${currentIndex * 100}%)` }}>
                {hasImageSlides
                    ? imageSlides.map((slide, index) => (
                        <article key={`${slide.alt}-${index}`} className="slide slide-image-only">
                            <img src={slide.src} alt={slide.alt} className="slide-image" />
                        </article>
                    ))
                    : slides.map((slide) => (
                        <article key={slide.id} className={`slide ${slide.theme}`}>
                            <div className="slide-content">
                                <h2>{slide.title}</h2>
                                <p>{slide.description}</p>
                                <a className="slideshow-link" href="/">
                                    {slide.cta}
                                </a>
                            </div>
                        </article>
                    ))}
            </div>

            <button className="slide-control slide-prev" onClick={prevSlide} aria-label="Previous slide">
                {'<'}
            </button>
            <button className="slide-control slide-next" onClick={nextSlide} aria-label="Next slide">
                {'>'}
            </button>

            <div className="slide-dots" aria-label="Slide indicators">
                {(hasImageSlides ? imageSlides : slides).map((slide, index) => (
                    <button
                        key={`dot-${index}`}
                        className={`slide-dot ${index === currentIndex ? "active" : ""}`}
                        onClick={() => setCurrentIndex(index)}
                        aria-label={`Go to slide ${index + 1}`}
                    />
                ))}
            </div>
        </section>
    );
}