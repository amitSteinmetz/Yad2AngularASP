export interface NavItem {
  label?: string;
  route: string;
  image?: string;
}

export interface PopularApartment extends NavItem {
  price: number;
  address: string;
}

export const CATEGORIES: readonly NavItem[] = [
  { label: 'נדל"ן', route: '/real-estate', image: 'images/categories/real-estate.png' },
  { label: 'דירות חדשות', route: '/new-apartments', image: 'images/categories/new-apartments.png' },
  { label: 'רכב', route: '/vehicles', image: 'images/categories/vehicles.png' },
  { label: 'יד שנייה', route: '/second-hand', image: 'images/categories/second-hand.png' },
];

export const POPULAR_APARTMENTS: readonly PopularApartment[] = [
  {
    label: 'נדל"ן',
    route: '/real-estate',
    image: 'images/apartments/apartment-1.jpeg',
    price: 100000,
    address: 'תל אביב',
  },
  {
    label: 'דירות חדשות',
    route: '/new-apartments',
    image: 'images/apartments/apartment-2.jpeg',
    price: 200000,
    address: 'תל אביב',
  },
  {
    label: 'רכב',
    route: '/vehicles',
    image: 'images/apartments/apartment-3.jpeg',
    price: 300000,
    address: 'תל אביב',
  },
];
