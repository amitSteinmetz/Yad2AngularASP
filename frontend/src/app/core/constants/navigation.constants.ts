export interface CategoryItem {
  label?: string;
  image?: string;
  options?: CategoryOption[];
}

export interface CategoryOption {
  category: string;
  title: string;
  route: string;
}

export const REAL_ESTATE_OPTIONS_LIST: CategoryOption[] = [
  { category: 'נדל"ן', title: 'דירות למכירה', route: '/real-estate/for-sale' },
  { category: 'נדל"ן', title: 'דירות להשכרה', route: '/real-estate/for-rent' },
  { category: 'נדל"ן', title: 'נדל"ן מסחרי', route: '/real-estate/commercial' },
  { category: 'נדל"ן', title: 'עסקים למכירה', route: '/real-estate/business-for-sale' },
  { category: 'נדל"ן', title: 'יד1 דירות חדשות', route: '/real-estate/new-homes' },
  { category: 'נדל"ן', title: 'כונס נכסים', route: '/real-estate/foreclosure' },
  { category: 'נדל"ן', title: 'משרדי תיווך בישראל', route: '/real-estate/brokers' },
  { category: 'נדל"ן', title: 'ייעוץ משכנתאות', route: '/real-estate/mortgage' },
  { category: 'נדל"ן', title: 'מדור ידאטה', route: '/real-estate/yadata' },
];

export const VEHICLES_OPTIONS_LIST: CategoryOption[] = [
  { category: 'רכב', title: 'פרטיים ומסחריים', route: '/vehicles/private-commercial' },
  { category: 'רכב', title: 'אופנועים', route: '/vehicles/motorcycles' },
  { category: 'רכב', title: 'קטנועים', route: '/vehicles/scooters' },
  { category: 'רכב', title: 'מיוחדים', route: '/vehicles/special' },
  { category: 'רכב', title: 'אביזרים', route: '/vehicles/accessories' },
  { category: 'רכב', title: 'משאיות', route: '/vehicles/trucks' },
  { category: 'רכב', title: 'כלי שיט', route: '/vehicles/boats' },
  { category: 'רכב', title: 'מחירון רכב', route: '/vehicles/price-list' },
  { category: 'רכב', title: 'מימון רכב', route: '/vehicles/financing' },
  { category: 'רכב', title: 'רכבים במכירה פומבית', route: '/vehicles/auction' },
  { category: 'רכב', title: 'בדיקת רכב לפני קנייה', route: '/vehicles/inspection' },
  { category: 'רכב', title: 'השוואת ביטוח רכב', route: '/vehicles/insurance-comparison' },
];

export const CATEGORIES: CategoryItem[] = [
  {
    label: 'נדל"ן',
    image: 'images/categories/real-estate.png',
    options: REAL_ESTATE_OPTIONS_LIST,
  },
  {
    label: 'דירות חדשות',
    image: 'images/categories/new-apartments.png',
  },
  {
    label: 'רכב',
    image: 'images/categories/vehicles.png',
    options: VEHICLES_OPTIONS_LIST,
  },
  {
    label: 'יד שנייה',
    image: 'images/categories/second-hand.png',
  },
];
