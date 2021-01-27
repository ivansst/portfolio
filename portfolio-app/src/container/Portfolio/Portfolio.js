import Navbar from "../../components/Navbar/Navbar";
import HeroSection from "../../components/HeroSection/HeroSection";
import CardsSection from "../../components/Cards/CardsSection";
import Experience from "../../components/Experience/Experience";

export default function Portfolio() {
  return (
    <>
      <Navbar />
      <HeroSection />
      <Experience />
      <CardsSection />
    </>
  );
}
