export interface NavItem {
  label?: string;
  route: string;
  image?: string;
}

export interface PopularApartment extends NavItem {
  price: number;
  address: string;
}

export const BANNERS = [
  {
    path: 'images/banners/banner-1.webp',
  },
  {
    path: 'images/banners/banner-2.webp',
  },
  {
    path: 'images/banners/banner-3.webp',
  },
];

export const POPULAR_APARTMENTS: readonly PopularApartment[] = [
  {
    label: 'דירת יוקרה במרכז',
    route: '/real-estate/1',
    image:
      'https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?auto=format&fit=crop&q=80&w=1000',
    price: 3500000,
    address: 'רוטשילד, תל אביב',
  },
  {
    label: 'דירת גן מעוצבת',
    route: '/real-estate/2',
    image:
      'https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?auto=format&fit=crop&q=80&w=1000',
    price: 2850000,
    address: 'הצפון הישן, תל אביב',
  },
  {
    label: 'פנטהאוז עם נוף לים',
    route: '/real-estate/3',
    image:
      'https://images.unsplash.com/photo-1512917774080-9991f1c4c750?auto=format&fit=crop&q=80&w=1000',
    price: 5200000,
    address: 'הירקון, תל אביב',
  },
];
