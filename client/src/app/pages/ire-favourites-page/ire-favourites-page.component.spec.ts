import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreFavouritesPageComponent } from './ire-favourites-page.component';

describe('IreFavouritesPageComponent', () => {
  let component: IreFavouritesPageComponent;
  let fixture: ComponentFixture<IreFavouritesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreFavouritesPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreFavouritesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
